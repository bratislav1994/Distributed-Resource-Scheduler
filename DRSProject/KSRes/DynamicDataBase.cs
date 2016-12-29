using CommonLibrary;
using CommonLibrary.Exceptions;
using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSRes
{
    public class DynamicDataBase
    {
        private Dictionary<string, string> registrationService = null;
        private List<LKResService> activeService = null;
        private List<IKSClient> clients = new List<IKSClient>();

        public Dictionary<string, string> RegistrationService
        {
            get
            {
                return registrationService;
            }
        }

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

        public DynamicDataBase()
        {
            registrationService = new Dictionary<string, string>();
            activeService = new List<LKResService>();
            clients = new List<IKSClient>();

            Thread CheckIfLKServiceIsAliveThread = new Thread(() => CheckIfLKServiceIsAlive());
            CheckIfLKServiceIsAliveThread.Start();
        }

        private LKResService GetServiceSID(string sessionID)
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
            foreach (LKResService service in ActiveService)
            {
                if (service.Username.Equals(username))
                {
                    return service;
                }
            }

            return null;
        }

        public void Registration(string username, string password)
        {
            if (registrationService.ContainsKey(username))
            {
                IdentificationExeption ex = new IdentificationExeption("Service is exist.");
                throw new FaultException<IdentificationExeption>(ex);
            }

            RegistrationService.Add(username, password);
        }

        public void Login(string username, string password, ILKRes channel, string sessionID)
        {
            if(registrationService.ContainsKey(username))
            {
                if( !registrationService[username].Equals(password))
                {
                    IdentificationExeption ex = new IdentificationExeption("Authentication error.");
                    throw new FaultException<IdentificationExeption>(ex);
                }

                foreach(LKResService service in activeService)
                {
                    if(service.Username.Equals(username))
                    {
                        IdentificationExeption ex = new IdentificationExeption("Service is already logged in.");
                        throw new FaultException<IdentificationExeption>(ex);
                    }
                }
                
                LKResService newService = new LKResService(username, channel, sessionID);
                ActiveService.Add(newService);
            }
            else
            {
                IdentificationExeption ex = new IdentificationExeption("Authentication error.");
                throw new FaultException<IdentificationExeption>(ex);
            }
        }

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
                foreach(Site site in sites)
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
            if(generators == null)
            {
                throw new InvalidOperationException();
            }

            bool edit = false;
            List<Generator> addGenerator = new List<Generator>();

            foreach(Generator newGenerator in generators)
            {
                foreach(Generator generator in service.Generators)
                {
                    if(newGenerator.MRID.Equals(generator.MRID))
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
                        break;
                    }
                }
                if(!edit)
                {
                    addGenerator.Add(newGenerator);
                }
            }

            service.Generators.AddRange(addGenerator);
        }

        private void RemoveSite(List<Site> sites, LKResService service)
        {
            List<Site> removeList = new List<Site>();
            if (sites != null)
            {
                foreach (Site removesite in sites)
                {
                    foreach(Site site in service.Sites)
                    {
                        if(removesite.MRID.Equals(site.MRID))
                        {
                            removeList.Add(site);
                        }
                    }
                }

                foreach(Site site in removeList)
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

        private void CheckIfLKServiceIsAlive()
        {
            List<LKResService> serviceForRemove = new List<LKResService>();

            foreach(LKResService user in ActiveService)
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

            foreach(LKResService user in serviceForRemove)
            {
                ActiveService.Remove(user);
            }

            serviceForRemove.Clear();

            Thread.Sleep(1000);
        }

        public void AddClient(IKSClient client)
        {
            Clients.Add(client);
        }

        private void NotifyClients(UpdateInfo update, string username)
        {
            foreach(IKSClient client in Clients)
            {
                client.Update(update, username);
            }
        }
    }
}
