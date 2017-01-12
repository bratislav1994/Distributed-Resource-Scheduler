//-----------------------------------------------------------------------
// <copyright file="Controler.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.ServiceModel;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using CommonLibrary;
    using CommonLibrary.Exceptions;
    using CommonLibrary.Interfaces;
    using KSRes.Access;
    using KSRes.Data;

    public class Controler
    {
        private Dictionary<string, string> registrationService = null;
        private List<LKResService> activeService = null;
        private List<IKSClient> clients = null;
        private Dictionary<string, List<ProductionHistory>> multiThreadBuffer = null;
        private object lockForMultiThreadBuffer = null;
        private object lockForActiveService = null;
        private ILoadForecast proxy;
        private SortedDictionary<DateTime, double> lastValuesLC = null;

        #region Constructor
        public Controler()
        {
            registrationService = new Dictionary<string, string>();
            activeService = new List<LKResService>();
            clients = new List<IKSClient>();
            lockForMultiThreadBuffer = new object();
            lockForActiveService = new object();
            multiThreadBuffer = new Dictionary<string, List<ProductionHistory>>();

            ChannelFactory<ILoadForecast> factory = new ChannelFactory<ILoadForecast>(
                new NetTcpBinding(),
                 new EndpointAddress("net.tcp://localhost:10040/ILoadForecast"));
            proxy = factory.CreateChannel();

            LastValuesLC = new SortedDictionary<DateTime, double>();
            
            Thread checkIfLKServiceIsAliveThread = new Thread(() => CheckIfLKServiceIsAlive());
            checkIfLKServiceIsAliveThread.Start();

            Thread processingDataThread = new Thread(() => ProcessingData());
            processingDataThread.Start();

            Thread loadForecastThread = new Thread(() => LoadForecastThread());
            loadForecastThread.Start();
        }
        #endregion Constructor

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

        public Dictionary<string, List<ProductionHistory>> MultiThreadBuffer
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

        #region GetService
        public LKResService GetServiceSID(string sessionID)
        {
            LKResService service = null;
            lock (lockForActiveService)
            {
                service = ActiveService.Where(o => o.SessionID.Equals(sessionID)).FirstOrDefault();
            }

            return service;
        }

        public LKResService GetService(string username)
        {
            LKResService service = null;
            lock (lockForActiveService)
            {
                service = ActiveService.Where(o => o.Username.Equals(username)).FirstOrDefault();
            }

            return service;
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

            LocalDB.Instance.Registration(new Data.RegisteredService()
            {
                Username = username,
                Password = HashAlgorithm.Create().ComputeHash(Encoding.ASCII.GetBytes(password))
            });
            Console.WriteLine("{0}\t User: {1} is registered.", DateTime.Now, username);
        }

        public void Login(string username, string password, ILKRes channel, string sessionID)
        {
            RegisteredService service = null;

            if ((service = LocalDB.Instance.GetService(username)) != null)
            {
                if (!service.Password.SequenceEqual(HashAlgorithm.Create().ComputeHash(Encoding.ASCII.GetBytes(password))))
                {
                    IdentificationExeption ex = new IdentificationExeption("Authentication error.");
                    throw new FaultException<IdentificationExeption>(ex);
                }

                lock (lockForActiveService)
                {
                    if (ActiveService.Where(o => o.Username.Equals(username)).FirstOrDefault() != null)
                    {
                        IdentificationExeption ex = new IdentificationExeption("Service is already logged in.");
                        throw new FaultException<IdentificationExeption>(ex);
                    }

                    LKResService newService = new LKResService(username, channel, sessionID);
                    Console.WriteLine("{0}\t User: {1} is logged in.", DateTime.Now, username);
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

            switch (update.UpdateType)
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
        #endregion Update

        #region ProcessingMeasurement
        public void SendMeasurement(string username, Dictionary<string, double> measurments)
        {
            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.UPDATE;
            update.Groups = null;
            update.Sites = null;

            LKResService service = GetService(username);

            foreach (string mrid in measurments.Keys)
            {
                ProductionHistory productionHistory;

                lock (lockForMultiThreadBuffer)
                {
                    if (!multiThreadBuffer.ContainsKey(mrid))
                    {
                        productionHistory = new ProductionHistory();
                        productionHistory.Username = username;
                        productionHistory.MRID = mrid;
                        productionHistory.ActivePower = measurments[mrid];
                        MultiThreadBuffer[mrid] = new List<ProductionHistory>();
                        MultiThreadBuffer[mrid].Add(productionHistory);
                    }
                    else
                    {
                        productionHistory = new ProductionHistory();
                        productionHistory.Username = username;
                        productionHistory.MRID = mrid;
                        productionHistory.ActivePower = measurments[mrid];
                        MultiThreadBuffer[mrid].Add(productionHistory);
                    }
                }

                Generator generator = service.Generators.Where(x => x.MRID.Equals(mrid)).FirstOrDefault();
                generator.ActivePower = measurments[mrid];

                update.Generators.Add(generator);
            }

            NotifyClients(update, username);
        }

        public void ProcessingData()
        {
            while (true)
            {
                Thread.Sleep(10000);
                DateTime time = DateTime.Now;
                lock (lockForMultiThreadBuffer)
                {
                    foreach (KeyValuePair<string, List<ProductionHistory>> kp in MultiThreadBuffer)
                    {
                        ProductionHistory p = new ProductionHistory();
                        p.TimeStamp = time;
                        p.MRID = kp.Key;
                        p.Username = kp.Value.First().Username;
                        p.ActivePower = AverageAP(kp.Value);
                        LocalDB.Instance.AddProductions(p);
                    }

                    MultiThreadBuffer.Clear();
                }
            }
        }
        
        #endregion ProcessingMeasurement

        #region DeployActivePower
        public List<Point> P(double requiredAP, bool isBasePoint)
        {
            List<Generator> remoteGenerators = new List<Generator>();
            List<Point> points = new List<Point>();
            List<Generator> generators = new List<Generator>();

            lock (lockForActiveService)
            {
                foreach (LKResService client in ActiveService)
                {
                    generators.AddRange(client.Generators);
                }
            }

            double activePower = 0;
            double diff = 0;
            
            foreach (Generator generator in generators)
            {
                activePower += generator.ActivePower;

                if (generator.WorkingMode == WorkingMode.REMOTE && !isBasePoint)
                {
                    remoteGenerators.Add(generator);
                }
                else if (isBasePoint)
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
                        Point point = new Point();

                        if (diff > diff1)
                        {
                            diff -= diff1;
                            point.GeneratorID = remoteGenerator.MRID;
                            point.Power = remoteGenerator.Pmax;
                            points.Add(point);
                        }
                        else if (diff <= diff1)
                        {
                            point.GeneratorID = remoteGenerator.MRID;
                            point.Power = remoteGenerator.ActivePower + diff;
                            diff = 0;
                            points.Add(point);
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
                        Point point = new Point();
                        if (diff != 0)
                        {
                            if (remoteGenerator.ActivePower <= diff)
                            {
                                point.GeneratorID = remoteGenerator.MRID;
                                point.Power = 0;
                                diff -= remoteGenerator.ActivePower;
                                points.Add(point);
                            }
                            else
                            {
                                if ((remoteGenerator.ActivePower - remoteGenerator.Pmin) >= diff)
                                {
                                    point.GeneratorID = remoteGenerator.MRID;
                                    point.Power = remoteGenerator.ActivePower - diff;
                                    diff = 0;
                                    points.Add(point);
                                    break;
                                }
                                else
                                {
                                    point.GeneratorID = remoteGenerator.MRID;
                                    point.Power = remoteGenerator.Pmin;
                                    diff -= remoteGenerator.ActivePower - remoteGenerator.Pmin;
                                    points.Add(point);
                                }
                            }
                        }
                    }
                }
            }

            return points;
        }

        public void DeploySetPoint(List<Point> points)
        {
            if (points.Count != 0)
            {
                lock (lockForActiveService)
                {
                    foreach (LKResService service in ActiveService)
                    {
                        List<Point> temp = new List<Point>();
                        foreach (Generator generator in service.Generators)
                        {
                            Point point = points.Where(x => x.GeneratorID.Equals(generator.MRID)).FirstOrDefault();

                            if (point != null)
                            {
                                temp.Add(point);
                            }
                        }

                        service.Client.SendSetPoint(temp);
                    }
                }
            }
        }
        #endregion DeployActivePower

        #region GetProductionHistory
        public SortedDictionary<DateTime, double> GetProductionHistory(double days)
        {
            SortedDictionary<DateTime, double> retVal = new SortedDictionary<DateTime, double>();
            DateTime condition = DateTime.Now.AddMinutes(0 - days);
            Dictionary<DateTime, List<double>> temp = new Dictionary<DateTime, List<double>>();
            List<ProductionHistory> productions = LocalDB.Instance.ReadProductions(condition);

            foreach (ProductionHistory production in productions)
            {
                if (!temp.ContainsKey(production.TimeStamp))
                {
                    temp[production.TimeStamp] = new List<double>();
                }

                temp[production.TimeStamp].Add(production.ActivePower);
            }

            foreach (KeyValuePair<DateTime, List<double>> kp in temp)
            {
                retVal.Add(kp.Key, SumActivePower(kp.Value));
            }

            return retVal;
        }

        private double SumActivePower(List<double> list)
        {
            double retVal = 0;
            if (list != null)
            {
                foreach (double d in list)
                {
                    retVal += d;
                }
            }

            return retVal;
        }
        #endregion GetProductionHistory

        #region Private add/update/remove
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
                        generator.GroupID = newGenerator.GroupID;
                        edit = true;
                        //break;
                    }
                }

                if (!edit)
                {
                    addGenerator.Add(newGenerator);
                }
            }

            service.Generators.AddRange(addGenerator);
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
        
        #endregion add/update/remove
        
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
                    lock (lockForActiveService)
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
            foreach (IKSClient client in Clients)
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

            foreach (IKSClient client in notActiveClient)
            {
                clients.Remove(client);
            }
        }

        #region LoadForecast
        private void LoadForecastThread()
        {
            while (true)
            {
                Thread.Sleep(10000);
                LoadForecast();
            }
        }

        private void LoadForecast()
        {
            List<ConsuptionHistory> consuptions = LocalDB.Instance.ReadConsuptions();
            if (consuptions.Count == 2)
            {
                List<KeyValuePair<DateTime, double>> parameter = new List<KeyValuePair<DateTime, double>>();
                foreach (ConsuptionHistory consuption in consuptions)
                {
                    parameter.Add(new KeyValuePair<DateTime, double>(consuption.TimeStamp, consuption.Consuption));
                }

                LastValuesLC = proxy.LoadForecast(parameter);

                Dictionary<string, Dictionary<int, List<Point>>> deployment = new Dictionary<string, Dictionary<int, List<Point>>>();
                int minute = 0;

                foreach (double value in LastValuesLC.Values)
                {
                    List<Point> basePoints = P(value, true);

                    lock (lockForActiveService)
                    {
                        foreach (LKResService service in ActiveService)
                        {
                            List<Point> temp = GetAllBasePointsForUser(service.Username, basePoints);

                            if (!deployment.ContainsKey(service.Username))
                            {
                                deployment.Add(service.Username, new Dictionary<int, List<Point>>());
                            }

                            deployment[service.Username].Add(minute, temp);
                        }
                    }
                    minute++;
                }

                lock (lockForActiveService)
                {
                    foreach (LKResService service in ActiveService)
                    {
                        service.Client.SendBasePoint(deployment[service.Username]);
                    }
                }
            }
        }
    

        private List<Point> GetAllBasePointsForUser(string username, List<Point> basePoints)
        {
            List<Point> temp = new List<Point>();
            foreach (Generator generator in GetService(username).Generators)
            {
                Point point = basePoints.Where(x => x.GeneratorID.Equals(generator.MRID)).FirstOrDefault();

                if (point != null)
                {
                    temp.Add(point);
                }
                else
                {
                    Point newPoint = new Point();
                    newPoint.GeneratorID = generator.MRID;
                    newPoint.Power = generator.ActivePower;
                    temp.Add(newPoint);
                }
            }

            return temp;
        }
        #endregion LoadForecast

        private double AverageAP(List<ProductionHistory> list)
        {
            double retVal = 0;
            foreach (ProductionHistory p in list)
            {
                retVal += p.ActivePower;
            }

            retVal /= list.Count;

            return retVal;
        }
    }
}
