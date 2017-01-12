// <copyright file="LKForClientService.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

using LKRes.Data;

namespace LKRes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;
    using CommonLibrary;
    using CommonLibrary.Exceptions;
    using CommonLibrary.Interfaces;
    using LKRes.Access;

    /// <summary>
    /// Class represent a LKRes server who communicating with LKResClient and KSRes module
    /// </summary>
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class LKForClientService : ILKForClient, ILKRes
    {
        private int basepointCounter = 0;
        Dictionary<int, List<Point>> basepointBuffer = new Dictionary<int, List<Point>>();
        private IActivePowerManagement proxy = null;
        /// <summary>
        /// Property to lock a shared resource
        /// </summary>
        private static object lockObj = new object();

        /// <summary>
        /// Temporary data base
        /// </summary>
        private UpdateInfo updateInfo = new UpdateInfo();

        /// <summary>
        /// proxy for KSRes module
        /// </summary>
        private IKSRes kSResProxy = null;

        /// <summary>
        /// proxy for LK client
        /// </summary>
        private ILKClient client = null;

        /// <summary>
        /// The thread that informs the client about changes
        /// </summary>
        private Thread notifyThread = null;

        public IKSRes KSResProxy
        {
            get { return kSResProxy; }
            set { kSResProxy = value; }
        }

        public ILKClient Client
        {
            get { return client; }
            set { client = value; }
        }

        public object LockObj
        {
            get { return lockObj; }
            set { lockObj = value; }
        }

        public UpdateInfo Updateinfo
        {
            get { return updateInfo; }
            set { updateInfo = value; }
        }

        public LKForClientService()
        {

            DuplexChannelFactory<IKSRes> ksResFactory = new DuplexChannelFactory<IKSRes>(
                new InstanceContext(this),
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:10010/IKSRes"));

            kSResProxy = ksResFactory.CreateChannel();

            ChannelFactory<IActivePowerManagement> factory = new ChannelFactory<IActivePowerManagement>(
               new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:3000/IActivePowerManagement"));

            proxy = factory.CreateChannel();

            Thread changePowerThread = new Thread(ChangeActivePower);
            changePowerThread.Start();

            Thread basePointThread = new Thread(() => SendBasePoint(basepointBuffer));
            basePointThread.Start();
        }

        public string Ping()
        {
            return "OK";
        }

        public void SendSetPoint(List<Point> setPoints)
        {
            lock (LockObj)
            {
                foreach (Point setpoint in setPoints)
                {
                    Generator generator = updateInfo.Generators.Where(gen => gen.MRID.Equals(setpoint.GeneratorID)).FirstOrDefault();
                    generator.SetPoint = setpoint.Power;
                }
            }
        }

        public void ChangeActivePower()
        {
            while (true)
            {
                Thread.Sleep(4000);
                Random randGenerator = new Random();

                Dictionary<string, double> powerForProcessing = new Dictionary<string, double>();
                foreach (Group groupIterator in updateInfo.Groups)
                {
                    List<Generator> generetors = updateInfo.Generators.Where(gen => gen.GroupID.Equals(groupIterator.MRID)).ToList();
                    foreach (Generator generatorIterator in generetors)
                    {
                        //samo radi ako je na lokalu
                        if (generatorIterator.SetPoint == -1 && generatorIterator.HasMeasurment)
                        {
                            double newPower = 0;
                            //povecaj za 10%
                            if (randGenerator.Next(0, 2) == 0)
                            {
                                newPower = generatorIterator.ActivePower + (generatorIterator.ActivePower / 10);
                                if (newPower >= generatorIterator.Pmin && newPower <= generatorIterator.Pmax)
                                {
                                    generatorIterator.ActivePower = newPower;
                                    DataBase.Instance.AddMeasurement(new Measurement()
                                    {
                                        ActivePower = generatorIterator.ActivePower,
                                        MRID = generatorIterator.MRID,
                                        TimeStamp = DateTime.Now
                                    });
                                }
                            }

                            else
                            {
                                newPower = generatorIterator.ActivePower - (generatorIterator.ActivePower / 10);
                                if (newPower >= generatorIterator.Pmin && newPower <= generatorIterator.Pmax)
                                {
                                    generatorIterator.ActivePower = newPower;
                                    DataBase.Instance.AddMeasurement(new Measurement()
                                    {
                                        ActivePower = generatorIterator.ActivePower,
                                        MRID = generatorIterator.MRID,
                                        TimeStamp = DateTime.Now
                                    });
                                }
                            }
                        }
                    }

                    //izracujan ukupnu snagu i broj reprezentativnih generatora 
                    double totalPower = 0;
                    int numberOfGeneratorsWithMeasurments = 0;
                    foreach (Generator genIt in generetors)
                    {
                        if (genIt.HasMeasurment)
                        {
                            totalPower += genIt.ActivePower;
                            numberOfGeneratorsWithMeasurments++;
                        }
                    }

                    //postavi snagu ne reprezentativnih generatrora na prosecnu vrednost reprezentativnih generatoa
                    double averagePower = totalPower / numberOfGeneratorsWithMeasurments;
                    foreach (Generator genIt in generetors)
                    {
                        if (!genIt.HasMeasurment && genIt.SetPoint == -1)
                        {
                            genIt.ActivePower = averagePower;
                        }
                    }

                    foreach (Generator genIt in generetors)
                    {
                        powerForProcessing.Add(genIt.MRID, genIt.ActivePower);
                    }
                }

                //posalji snagu modulu 2
                if (powerForProcessing.Count != 0)
                {
                    lock (LockObj)
                    {
                        kSResProxy.SendMeasurement(powerForProcessing);
                    }
                }

                if (updateInfo.Generators.Count != 0)
                {
                    UpdateInfo update = new UpdateInfo();
                    update.UpdateType = UpdateType.UPDATE;
                    update.Groups = null;
                    update.Sites = null;

                    update.Generators = updateInfo.Generators;
                    client.Update(update);
                }
            }
        }

        public UpdateInfo GetMySystem()
        {
            updateInfo = DataBase.Instance.ReadData();
            KSResProxy.Update(updateInfo);
            OperationContext context = OperationContext.Current;
            client = context.GetCallbackChannel<ILKClient>();
            return updateInfo;
        }

        public void Login(string username, string password)
        {
            try
            {
                kSResProxy.Login(username, password);
            }
            catch (FaultException<IdentificationExeption> ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                throw ex;
            }
        }

        public void Registration(string username, string password)
        {
            try
            {
                kSResProxy.Registration(username, password);
            }
            catch (FaultException<IdentificationExeption> ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                throw ex;
            }

        }

        public void Update(UpdateInfo update)
        {
            if (update == null)
            {
                throw new ArgumentNullException("Update can't be null!");
            }

            switch (update.UpdateType)
            {
                case UpdateType.ADD:
                    Add(update);
                    break;
                case UpdateType.REMOVE:
                    Remove(update);
                    break;
                case UpdateType.UPDATE:
                    UpdateData(update);
                    break;
            }
            updateInfo = DataBase.Instance.ReadData();

            kSResProxy.Update(update);
            notifyThread = new Thread(() => NotifyClient(update));
            notifyThread.Start();
        }

        #region Add
        private void Add(UpdateInfo update)
        {
            update.Generators[0].MRID = Guid.NewGuid().ToString().Substring(0, 10);

            if (update.Groups != null)
            {
                update.Groups[0].MRID = Guid.NewGuid().ToString().Substring(0, 10);
            }

            if (update.Sites != null)
            {
                update.Sites[0].MRID = Guid.NewGuid().ToString().Substring(0, 10);
            }

            //dodavanje novog generatora, grupe i sajta
            if (update.Generators != null && update.Groups != null && update.Sites != null)
            {
                update.Generators[0].GroupID = update.Groups[0].MRID;
                update.Groups[0].SiteID = update.Sites[0].MRID;

                //updateInfo.Generators.Add(update.Generators[0]);
                //updateInfo.Sites.Add(update.Sites[0]);
                //updateInfo.Groups.Add(update.Groups[0]);

                DataBase.Instance.AddGenerator(new Data.GeneratorEntity()
                {
                    Gen = update.Generators[0]
                });
                DataBase.Instance.AddGroup(new Data.GroupEntity()
                {
                    GEntity = update.Groups[0]
                });
                DataBase.Instance.AddSite(new Data.SiteEntity()
                {
                    SEntity = update.Sites[0]
                });
            }
            else if (update.Generators != null && update.Groups != null && update.Sites == null)
            {
                update.Generators[0].GroupID = update.Groups[0].MRID;

                //updateInfo.Generators.Add(update.Generators[0]);
                //updateInfo.Groups.Add(update.Groups[0]);

                DataBase.Instance.AddGenerator(new Data.GeneratorEntity()
                {
                    Gen = update.Generators[0]
                });
                DataBase.Instance.AddGroup(new Data.GroupEntity()
                {
                    GEntity = update.Groups[0]
                });
            }
            else
            {
                //updateInfo.Generators.Add(update.Generators[0]);
                DataBase.Instance.AddGenerator(new Data.GeneratorEntity()
                {
                    Gen = update.Generators[0]
                });
            }

            Measurement m = new Measurement();
            m.ActivePower = update.Generators[0].ActivePower;
            m.MRID = update.Generators[0].MRID;
            m.TimeStamp = DateTime.Now;
            DataBase.Instance.AddMeasurement(m);
        }
        #endregion

        #region Remove
        private void Remove(UpdateInfo update)
        {
            Generator gen = null;
            gen = updateInfo.Generators.Where(g => g.MRID.Equals(update.Generators[0].MRID)).FirstOrDefault();

            if (gen != null)
            {
                //updateInfo.Generators.Remove(gen);
                DataBase.Instance.RemoveGenerator(gen);
            }

            if (update.Groups != null)
            {
                Group group = null;
                group = updateInfo.Groups.Where(g => g.MRID.Equals(update.Groups[0].MRID)).FirstOrDefault();
                // updateInfo.Groups.Remove(group);
                DataBase.Instance.RemoveGroup(group);
            }

            if (update.Sites != null)
            {
                Site site = null;
                site = updateInfo.Sites.Where(s => s.MRID.Equals(update.Sites[0].MRID)).FirstOrDefault();
                //updateInfo.Sites.Remove(site);
                DataBase.Instance.RemoveSite(site);
            }
        }
        #endregion

        #region Update
        private void UpdateData(UpdateInfo update)
        {

            DataBase.Instance.UpdateGenerator(update.Generators[0]);

            //nova grupa
            if (update.Groups != null)
            {
                DataBase.Instance.AddGroup(new Data.GroupEntity()
                {
                    GEntity = update.Groups[0]
                });
            }

            //novi sajt
            if (update.Sites != null)
            {
                DataBase.Instance.AddSite(new Data.SiteEntity()
                {
                    SEntity = update.Sites[0]
                });
            }
        }
        #endregion

        #region GetMeasurement

        public SortedDictionary<DateTime, double> GetMeasurements(string mRID)
        {
            return DataBase.Instance.GetMeasurements(mRID);
        }

        #endregion

        private void NotifyClient(UpdateInfo update)
        {
            Thread.Sleep(50);
            client.Update(update);
        }

        public void SendBasePoint(Dictionary<int, List<Point>> basePoints)
        {
            if (basePoints.Count != 0)
            {
                // List<Point> points = basePoints[i];
                lock (lockObj)
                {
                    basepointBuffer = basePoints;
                    basepointCounter = 0;
                }
            }
        }

        public void WriteBasePoint()
        {
            while (true)
            {
                if (basepointCounter < basepointBuffer.Count)
                {
                    List<Point> points = basepointBuffer[basepointCounter];
                    foreach (Point g in points)
                    {
                        Generator generator = updateInfo.Generators.Where(gen => gen.MRID.Equals(g.GeneratorID)).FirstOrDefault();
                        if (generator != null)
                        {
                            generator.BasePoint = g.Power;
                            DataBase.Instance.UpdateGenerator(generator);
                        }
                    }

                    basepointCounter++;
                }

                Thread.Sleep(60000);
            }
        }
    }
}


