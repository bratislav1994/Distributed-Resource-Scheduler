using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary.Interfaces;
using CommonLibrary;
using CommonLibrary.Exceptions;
using System.ServiceModel;
using System.Threading;

namespace LKRes.Services
{
    public class LKForClientService : ILKForClient, ILKRes
    {
        public static UpdateInfo updateInfo = new UpdateInfo();
        private IKSRes kSResProxy = null;
        private ILKClient client = null;
        Thread notifyThread = null;
        public LKForClientService()
        {
            try
            {
                DuplexChannelFactory<IKSRes> ksResFactory = new DuplexChannelFactory<IKSRes>(
                    new InstanceContext(this),
                    new NetTcpBinding(),
                    new EndpointAddress("net.tcp://localhost:10010/IKSRes"));

                kSResProxy = ksResFactory.CreateChannel();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: {0}.", ex.Message);
            }
            
            Thread ChangePowerThread = new Thread(ChangeActivePower);
            ChangePowerThread.Start();
        }

        public string Ping()
        {
            return "OK";
        }

        public void SendSetPoint(List<SetPoint> setPoints)
        {
            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.UPDATE;
            update.Groups = null;
            update.Sites = null;
            foreach (SetPoint setpoint in setPoints)
            {
                Generator generator = LKForClientService.updateInfo.Generators.Where(gen => gen.MRID.Equals(setpoint.GeneratorID)).FirstOrDefault();
                generator.SetPoint = setpoint.Setpoint;
                update.Generators.Add(generator);
            }
            Thread.Sleep(2500);
            client.Update(update);
        }

        public void ChangeActivePower()
        {
            Random randGenerator = new Random();
            while (true)
            {
                Thread.Sleep(4000);

                foreach (Group groupIterator in updateInfo.Groups)
                {
                    List<Generator> generetors = updateInfo.Generators.Where(gen => gen.GroupID.Equals(groupIterator.MRID)).ToList();
                    foreach (Generator generatorIterator in generetors)
                    {
                        //samo radi ako je na lokalu
                        if (generatorIterator.WorkingMode == WorkingMode.LOCAL && generatorIterator.HasMeasurment)
                        {
                            //povecaj za 10%
                            if (randGenerator.Next(0, 1) == 0)
                            {
                                generatorIterator.ActivePower = generatorIterator.ActivePower + (generatorIterator.ActivePower / 10);
                            }
                            //smanji za 10%
                            else
                            {
                                generatorIterator.ActivePower = generatorIterator.ActivePower - (generatorIterator.ActivePower / 10);
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
                        if (!genIt.HasMeasurment)
                        {
                            genIt.ActivePower = averagePower;
                        }
                    }

                    Dictionary<string, double> powerForProcessing = new Dictionary<string, double>(generetors.Count);
                    foreach (Generator genIt in generetors)
                    {
                        powerForProcessing.Add(genIt.MRID, genIt.ActivePower);
                    }

                    //posalji snagu modulu 2
                    //kSResProxy.SendMeasurement(powerForProcessing);
                }

            }
        }

        public UpdateInfo GetMySystem()
        {
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
                throw new ArgumentNullException("Update can't be null!");

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
            kSResProxy.Update(update);
            notifyThread = new Thread(() => NotifyClient(update));
            notifyThread.Start();
        }

        public void Add(UpdateInfo update)
        {
            if(update.Generators != null)
                update.Generators[0].MRID = Guid.NewGuid().ToString();

            if(update.Groups != null)
                update.Groups[0].MRID = Guid.NewGuid().ToString();

            if(update.Sites != null)
                update.Sites[0].MRID = Guid.NewGuid().ToString();

            //dodavanje novog generatora, grupe i sajta
            if (update.Generators != null && update.Groups != null && update.Sites != null)
            {
                update.Generators[0].GroupID = update.Groups[0].MRID;
                update.Groups[0].SiteID = update.Sites[0].MRID;

                updateInfo.Generators.Add(update.Generators[0]);
                updateInfo.Sites.Add(update.Sites[0]);
                updateInfo.Groups.Add(update.Groups[0]);
            }
            //dodavanje novog generatora i grupe. Sajt vec postoji.
            else if (update.Generators != null && update.Groups != null && update.Sites == null)
            {
                update.Generators[0].GroupID = update.Groups[0].MRID;

                updateInfo.Generators.Add(update.Generators[0]);
                updateInfo.Groups.Add(update.Groups[0]);
            } 
        }

        public void Remove(UpdateInfo update)
        {
            Generator gen = null;
            gen = updateInfo.Generators.Where(g => g.MRID.Equals(update.Generators[0].MRID)).FirstOrDefault();

            if(gen != null)
                updateInfo.Generators.Remove(gen);

            bool areEquals = false;
            foreach (Group gIterator in updateInfo.Groups)
            {
                if (gIterator.MRID.Equals(update.Generators[0].GroupID))
                {
                    areEquals = true;
                    break;
                }  
            }

            if (!areEquals)
            {
                updateInfo.Groups.Remove(update.Groups[0]);
            }
            else
            {
                areEquals = false;
            }

            foreach (Site sIterator in updateInfo.Sites)
            {
                if (sIterator.MRID.Equals(update.Groups[0].SiteID))
                {
                    areEquals = true;
                    break;
                }
            }

            if (!areEquals)
            {
                updateInfo.Sites.Remove(update.Sites[0]);
            }
        }

        public void UpdateData(UpdateInfo update)
        {
            Generator generator = updateInfo.Generators.Where(gen => gen.MRID.Equals(update.Generators[0].MRID)).FirstOrDefault();
            int index = updateInfo.Generators.IndexOf(generator);

            if (index != -1)
                updateInfo.Generators[index] = update.Generators[0];

            //nova grupa
            if (update.Groups[0] != null)
            {
                updateInfo.Groups.Add(update.Groups[0]);
            }

            //novi sajt
            if (update.Sites[0] != null)
            {
                updateInfo.Sites.Add(update.Sites[0]);
            }
        }

        private void NotifyClient(UpdateInfo update)
        {
            Thread.Sleep(50);
            client.Update(update);
        }
    }
}
