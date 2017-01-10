using CommonLibrary;
using CommonLibrary.Exceptions;
using CommonLibrary.Interfaces;
using KSRes.Access;
using KSRes.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace KSRes
{
    public class Controler
    {
        private Dictionary<string, string> registrationService = null;
        private List<LKResService> activeService = null;
        private List<IKSClient> clients = null;
        private List<ProductionHistory> multiThreadBuffer = null;
        private object lockObj = null;
        private object lockObj1 = new object();
        private ILoadForecast proxy;
        private SortedDictionary<DateTime, double> lastValuesLC = null;

        #region Property
        public List<LKResService> ActiveService
        {
            get
            {
                return activeService;
            }
        }

        public List<IKSClient> Clients
        {
            get
            {
                return clients;
            }
        }

        public List<ProductionHistory> MultiThreadBuffer
        {
            get
            {
                return multiThreadBuffer;
            }
        }

        public SortedDictionary<DateTime, double> LastValuesLC
        {
            get
            {
                return lastValuesLC;
            }

            set
            {
                lastValuesLC = value;
            }
        }
        #endregion Property

        #region Constructor
        public Controler()
        {
            registrationService = new Dictionary<string, string>();
            activeService = new List<LKResService>();
            clients = new List<IKSClient>();
            lockObj = new object();
            multiThreadBuffer = new List<ProductionHistory>();

            ChannelFactory<ILoadForecast> factory = new ChannelFactory<ILoadForecast>(
                new NetTcpBinding(),
                 new EndpointAddress("net.tcp://localhost:10040/ILoadForecast"));
            proxy = factory.CreateChannel();

            LastValuesLC = new SortedDictionary<DateTime, double>();

            Thread CheckIfLKServiceIsAliveThread = new Thread(() => CheckIfLKServiceIsAlive());
            //CheckIfLKServiceIsAliveThread.Start();

            Thread ProcessingDataThread = new Thread(() => ProcessingData());
            ProcessingDataThread.Start();

            Thread LoadForecastThread = new Thread(() => LoadForecast());
            LoadForecastThread.Start();
        }
        #endregion Constructor

        #region GetService
        public LKResService GetServiceSID(string sessionID)
        {
            foreach (LKResService service in ActiveService)
            {
                if (service.SessionID.Equals(sessionID))
                {
                    return service;
                }
            }

            return null;
        }

        public LKResService GetService(string username)
        {
            LKResService retVal = null;

            foreach (LKResService service in ActiveService)
            {
                if (service.Username.Equals(username))
                {
                    retVal = service;
                    break;
                }
            }

            return retVal;
        }
        #endregion GetService

        #region Registration/Login
        public void Registration(string username, string password)
        {
            if (LocalDB.Instance.GetService(username) != null)
            {
                IdentificationExeption ex = new IdentificationExeption("Service already exists.");
                throw new FaultException<IdentificationExeption>(ex);
            }

            lock (lockObj)
            {
                byte[] hash = HashAlgorithm.Create().ComputeHash(Encoding.ASCII.GetBytes(password));
                LocalDB.Instance.Registration(new Data.RegisteredService()
                {
                    Username = username,
                    Password = hash
                });
            }
        }

        public void Login(string username, string password, ILKRes channel, string sessionID)
        {
            RegisteredService service = null;

            if((service = LocalDB.Instance.GetService(username)) != null)
            {
                if (!service.Password.SequenceEqual(HashAlgorithm.Create().ComputeHash(Encoding.ASCII.GetBytes(password))))
                {
                    IdentificationExeption ex = new IdentificationExeption("Authentication error.");
                    throw new FaultException<IdentificationExeption>(ex);
                }

                foreach(LKResService service1 in activeService)
                {
                    if(service1.Username.Equals(username))
                    {
                        IdentificationExeption ex = new IdentificationExeption("Service is already logged in.");
                        throw new FaultException<IdentificationExeption>(ex);
                    }
                }
                
                LKResService newService = new LKResService(username, channel, sessionID);
                lock (lockObj)
                {
                    Console.WriteLine("User: {0} je logovan.", username);
                    ActiveService.Add(newService);
                }
            }
            else
            {
                IdentificationExeption ex = new IdentificationExeption("Authentication error.");
                throw new FaultException<IdentificationExeption>(ex);
            }
        }
        #endregion Registration/Login

        #region Update
        public void Update(string sessionID, UpdateInfo update)
        {
            LKResService serviceUp = GetServiceSID(sessionID);

            if (serviceUp == null)
            {
                IdentificationExeption ex = new IdentificationExeption("Service is not authenticate");
                throw new FaultException<IdentificationExeption>(ex);
            }

            if (update == null)
            {
                throw new InvalidDataException();
            }

            switch(update.UpdateType)
            {
                case UpdateType.ADD:
                case UpdateType.UPDATE:
                    AddOrUpdateSite(update.Sites, serviceUp);
                    AddOrUpdateGroup(update.Groups, serviceUp);
                    AddOrUpdateGenerator(update.Generators, serviceUp);     
                    break;
                case UpdateType.REMOVE:
                    RemoveSite(update.Sites, serviceUp);
                    RemoveGroup(update.Groups, serviceUp);
                    RemoveGenerator(update.Generators, serviceUp);
                    break;
            }

            NotifyClients(update, serviceUp.Username);
        }

        #region private add/update/remove
        private void AddOrUpdateSite(List<Site> sites, LKResService service)
        {
            if (sites != null)
            {
                foreach (Site site in sites)
                {
                    service.Sites.Add(site);
                }
            }
        }

        private void AddOrUpdateGroup(List<Group> groups, LKResService service)
        {
            if (groups != null)
            {
                foreach (Group group in groups)
                {
                    service.Gropus.Add(group);
                }
            }
        }

        private void AddOrUpdateGenerator(List<Generator> generators, LKResService service)
        {
            if (generators == null)
            {
                throw new InvalidDataException();
            }

            bool edit = false;
            List<Generator> addGenerator = new List<Generator>();

            foreach (Generator newGenerator in generators)
            {
                foreach (Generator generator in service.Generators)
                {
                    if (newGenerator.MRID.Equals(generator.MRID))
                    {
                        generator.HasMeasurment = newGenerator.HasMeasurment;
                        generator.Name = newGenerator.Name;
                        generator.Pmax = newGenerator.Pmax;
                        generator.Pmin = newGenerator.Pmin;
                        generator.Price = newGenerator.Price;
                        generator.WorkingMode = newGenerator.WorkingMode;
                        generator.ActivePower = newGenerator.ActivePower;
                        generator.SetPoint = newGenerator.SetPoint;
                        generator.BasePoint = newGenerator.BasePoint;
                        generator.GeneratorType = newGenerator.GeneratorType;

                        edit = true;
                        //break;
                    }
                }
                if (!edit)
                {
                    addGenerator.Add(newGenerator);
                }
            }

            //service.Generators.AddRange(addGenerator);
            foreach (Generator generator in addGenerator)
            {
                service.Generators.Add(generator);
            }
        }

        private void RemoveSite(List<Site> sites, LKResService service)
        {
            List<Site> removeList = new List<Site>();
            if (sites != null)
            {
                foreach (Site removesite in sites)
                {
                    foreach (Site site in service.Sites)
                    {
                        if (removesite.MRID.Equals(site.MRID))
                        {
                            removeList.Add(site);
                        }
                    }
                }

                foreach (Site site in removeList)
                {
                    service.Sites.Remove(site);
                }
            }
        }

        private void RemoveGroup(List<Group> groups, LKResService service)
        {
            List<Group> removeList = new List<Group>();
            if (groups != null)
            {
                foreach (Group removesite in groups)
                {
                    foreach (Group group in service.Gropus)
                    {
                        if (removesite.MRID.Equals(group.MRID))
                        {
                            removeList.Add(group);
                        }
                    }
                }

                foreach (Group group in removeList)
                {
                    service.Gropus.Remove(group);
                }
            }
        }

        private void RemoveGenerator(List<Generator> generators, LKResService service)
        {
            List<Generator> removeList = new List<Generator>();
            if (generators != null)
            {
                foreach (Generator removesite in generators)
                {
                    foreach (Generator generator in service.Generators)
                    {
                        if (removesite.MRID.Equals(generator.MRID))
                        {
                            removeList.Add(generator);
                        }
                    }
                }

                foreach (Generator generator in removeList)
                {
                    service.Generators.Remove(generator);
                }
            }
        }
        #endregion add/update/remove
        #endregion Update

        public void SendMeasurement(string username, Dictionary<string, double> measurments)
        {
            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.UPDATE;
            update.Groups = null;
            update.Sites = null;

            LKResService service = GetService(username);

            foreach (string mrid in measurments.Keys)
            {
                ProductionHistory productionHistory = new ProductionHistory();
                productionHistory.Username = username;
                productionHistory.MRID = mrid;
                productionHistory.ActivePower = measurments[mrid];

                lock (lockObj1)
                {
                    MultiThreadBuffer.Add(productionHistory);
                }

                Generator generator = service.Generators.Where(x => x.MRID.Equals(mrid)).FirstOrDefault();
                generator.ActivePower = measurments[mrid];

                update.Generators.Add(generator);
            }  

            foreach(IKSClient client in clients)
            {
                client.Update(update, username);
            }
        }

        public void ProcessingData()
        {
            while (true)
            {
                Thread.Sleep(10000);

                foreach (ProductionHistory productionHistory in MultiThreadBuffer)
                {
                    LocalDB.Instance.AddProductions(productionHistory);
                }

                lock (lockObj1)
                {
                    MultiThreadBuffer.Clear();
                }
            }
        }

        private void CheckIfLKServiceIsAlive()
        {
            while (true)
            {
                List<LKResService> serviceForRemove = new List<LKResService>();

                foreach (LKResService user in ActiveService)
                {
                    try
                    {
                        user.Client.Ping();
                    }
                    catch
                    {
                        serviceForRemove.Add(user);
                    }
                }

                foreach (LKResService user in serviceForRemove)
                {
                    lock (lockObj)
                    {
                        ActiveService.Remove(user);
                    }
                }

                serviceForRemove.Clear();

                Thread.Sleep(1000);
            }
        }

        private void NotifyClients(UpdateInfo update, string username)
        {
            List<IKSClient> notActiveClient = new List<IKSClient>();
            foreach(IKSClient client in Clients)
            {
                try
                {
                    client.Update(update, username);
                }
                catch
                {
                    notActiveClient.Add(client);
                }
            }

            foreach(IKSClient client in notActiveClient)
            {
                lock (lockObj)
                {
                    clients.Remove(client);
                }
            }
        }

        #region DeployActivePower
        public List<SetPoint> P(double requiredAP)
        {
            List<Generator> remoteGenerators = new List<Generator>();
            List<SetPoint> setPoints = new List<SetPoint>();
            List<Generator> generators = new List<Generator>();

            foreach (LKResService client in ActiveService)
            {
                generators.AddRange(client.Generators);
            }

            double activePower = 0;
            double diff = 0;

            foreach (Generator generator in generators)
            {
                activePower += generator.ActivePower;

                if (generator.WorkingMode == WorkingMode.REMOTE)
                {
                    remoteGenerators.Add(generator);
                }
            }

            diff = requiredAP - activePower;

            if (diff > 0)
            {
                List<Generator> sortedList = remoteGenerators.OrderBy(o => o.Price).ToList();
                foreach (Generator remoteGenerator in sortedList)
                {
                    double diff1 = 0;
                    if ((diff1 = remoteGenerator.Pmax - remoteGenerator.ActivePower) > 0)
                    {
                        SetPoint setPoint = new SetPoint();

                        if (diff > diff1)
                        {
                            diff -= diff1;
                            setPoint.GeneratorID = remoteGenerator.MRID;
                            setPoint.Setpoint = remoteGenerator.Pmax;
                            setPoints.Add(setPoint);
                        }
                        else if (diff <= diff1)
                        {
                            setPoint.GeneratorID = remoteGenerator.MRID;
                            setPoint.Setpoint = remoteGenerator.ActivePower + diff;
                            diff = 0;
                            setPoints.Add(setPoint);
                            break;
                        }
                    }
                }
            }
            else
            {
                diff = Math.Abs(diff);
                List<Generator> sortedList = remoteGenerators.OrderByDescending(o => o.Price).ToList();
                foreach (Generator remoteGenerator in sortedList)
                {
                    if (remoteGenerator.ActivePower > 0)
                    {
                        SetPoint setPoint = new SetPoint();
                        if (diff != 0)
                        {
                            if (remoteGenerator.ActivePower <= diff)
                            {
                                setPoint.GeneratorID = remoteGenerator.MRID;
                                setPoint.Setpoint = 0;
                                diff -= remoteGenerator.ActivePower;
                                setPoints.Add(setPoint);
                            }
                            else
                            {
                                if ((remoteGenerator.ActivePower - remoteGenerator.Pmin) >= diff)
                                {
                                    setPoint.GeneratorID = remoteGenerator.MRID;
                                    setPoint.Setpoint = remoteGenerator.ActivePower - diff;
                                    diff = 0;
                                    setPoints.Add(setPoint);
                                    break;
                                }
                                else
                                {
                                    setPoint.GeneratorID = remoteGenerator.MRID;
                                    setPoint.Setpoint = remoteGenerator.Pmin;
                                    diff -= remoteGenerator.ActivePower - remoteGenerator.Pmin;
                                    setPoints.Add(setPoint);
                                }
                            }
                        }
                    }
                }
            }
            return setPoints;
        }

        public void DeploySetPoint(List<SetPoint> setPoints)
        {
            if (setPoints.Count != 0)
            {
                foreach (LKResService service in ActiveService)
                {
                    List<SetPoint> temp = new List<SetPoint>();
                    foreach (Generator generator in service.Generators)
                    {
                        SetPoint setPoint = setPoints.Where(x => x.GeneratorID.Equals(generator.MRID)).FirstOrDefault();

                        if (setPoint != null)
                        {
                            temp.Add(setPoint);
                        }
                    }
                    service.Client.SendSetPoint(temp);
                }
            }
        }
        #endregion DeployActivePower

        #region LoadForecast
        private void LoadForecast()
        {
            while (true)
            {
                Thread.Sleep(10000);
                List<ConsuptionHistory> consuptions = LocalDB.Instance.ReadConsuptions();
                if (consuptions.Count == 2)
                {
                    List<KeyValuePair<DateTime, double>> parameter = new List<KeyValuePair<DateTime, double>>();
                    foreach (ConsuptionHistory consuption in consuptions)
                    {
                        parameter.Add(new KeyValuePair<DateTime, double>(consuption.TimeStamp, consuption.Consuption));
                    }

                    LastValuesLC = proxy.LoadForecast(parameter);
                }
            }
        }
        #endregion LoadForecast
    }
}
