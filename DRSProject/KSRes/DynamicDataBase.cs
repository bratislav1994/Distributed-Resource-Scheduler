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
        private Dictionary<string, string> registrationUsers = null;
        private List<LKResService> activeUsers = null;

        public DynamicDataBase()
        {
            registrationUsers = new Dictionary<string, string>();
            activeUsers = new List<LKResService>();

            Thread CheckIfLKClientIsAliveThread = new Thread(() => CheckIfLKClientIsAlive());
            CheckIfLKClientIsAliveThread.Start();
        }

        public bool CheckRegistrationUser(string username)
        {
            string regUser = null;

            if (registrationUsers.TryGetValue(username, out regUser))
            {
                return true;
            }
            return false;
        }

        public LKResService GetUserSID(string sessionID)
        {
            foreach (LKResService user in activeUsers)
            {
                if (user.SessionID.Equals(sessionID))
                {
                    return user;
                }
            }

            return null;
        }

        public LKResService GetUser(string username)
        {
            foreach (LKResService user in activeUsers)
            {
                if (user.Username.Equals(username))
                {
                    return user;
                }
            }

            return null;
        }

        public void Registration(string username, string password)
        {
            if (CheckRegistrationUser(username))
            {
                IdentificationExeption ex = new IdentificationExeption("User is exist.");
                throw new FaultException<IdentificationExeption>(ex);
            }

            registrationUsers.Add(username, password);
        }

        public void Login(string username, string password, ILKRes channel, string sessionID)
        {
            foreach (LKResService user in activeUsers)
            {
                if (user.Username.Equals(username))
                {
                    if (!registrationUsers[username].Equals(password))
                    {
                        IdentificationExeption ex = new IdentificationExeption("Password is not correct.");
                        throw new FaultException<IdentificationExeption>(ex);
                    }

                    LKResService newUser = new LKResService(username, channel, sessionID);
                }
            }
        }

        public void Logout(string username)
        {
            foreach (LKResService user in activeUsers)
            {
                if (user.Username.Equals(username))
                {
                    activeUsers.Remove(user);
                }
            }
        }

        public void Update(string sessionID, UpdateInfo update)
        {
            LKResService userUp = GetUserSID(sessionID);

            if (userUp == null)
            {
                IdentificationExeption ex = new IdentificationExeption("User is not authenticate");
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
                    AddOrUpdateSite(update.Sites, userUp);
                    AddOrUpdateGroup(update.Groups, userUp);
                    AddOrUpdateGenerator(update.Generators, userUp);
                    break;
                case UpdateType.REMOVE:
                    RemoveSite(update.Sites, userUp);
                    RemoveGroup(update.Groups, userUp);
                    RemoveGenerator(update.Generators, userUp);
                    break;
            }
        }

        #region add/update/remove
        public void AddOrUpdateSite(List<Site> sites, LKResService user)
        {
            if (sites != null)
            {
                foreach(Site site in sites)
                {
                    user.Sites.Add(site);
                }
            }
        }

        public void AddOrUpdateGroup(List<Group> groups, LKResService user)
        {
            if (groups != null)
            {
                foreach (Group group in groups)
                {
                    user.Gropus.Add(group);
                }
            }
        }

        public void AddOrUpdateGenerator(List<Generator> generators, LKResService user)
        {
            if(generators == null)
            {
                throw new InvalidOperationException();
            }

            bool edit = false;
            List<Generator> addGenerator = new List<Generator>();

            foreach(Generator newGenerator in generators)
            {
                foreach(Generator generator in user.Generators)
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

            user.Generators.AddRange(addGenerator);
        }

        public void RemoveSite(List<Site> sites, LKResService user)
        {
            List<Site> removeList = new List<Site>();
            if (sites != null)
            {
                foreach (Site removesite in sites)
                {
                    foreach(Site site in user.Sites)
                    {
                        if(removesite.MRID.Equals(site.MRID))
                        {
                            removeList.Add(site);
                        }
                    }
                }

                foreach(Site site in removeList)
                {
                    user.Sites.Remove(site);
                }
            }
        }

        public void RemoveGroup(List<Group> groups, LKResService user)
        {
            List<Group> removeList = new List<Group>();
            if (groups != null)
            {
                foreach (Group removesite in groups)
                {
                    foreach (Group group in user.Gropus)
                    {
                        if (removesite.MRID.Equals(group.MRID))
                        {
                            removeList.Add(group);
                        }
                    }
                }

                foreach (Group group in removeList)
                {
                    user.Gropus.Remove(group);
                }
            }
        }

        public void RemoveGenerator(List<Generator> generators, LKResService user)
        {
            List<Generator> removeList = new List<Generator>();
            if (generators != null)
            {
                foreach (Generator removesite in generators)
                {
                    foreach (Generator generator in user.Generators)
                    {
                        if (removesite.MRID.Equals(generator.MRID))
                        {
                            removeList.Add(generator);
                        }
                    }
                }

                foreach (Generator generator in removeList)
                {
                    user.Generators.Remove(generator);
                }
            }
        }
        #endregion add/update/remove

        public List<LKResService> GetAllSystem()
        {
            return activeUsers;
        }

        private void CheckIfLKClientIsAlive()
        {
            List<LKResService> userForRemove = new List<LKResService>();

            foreach(LKResService user in activeUsers)
            {
                try
                {
                    user.Client.Ping();
                }
                catch
                {
                    userForRemove.Add(user);
                }
            }

            foreach(LKResService user in userForRemove)
            {
                activeUsers.Remove(user);
            }

            userForRemove.Clear();

            Thread.Sleep(1000);
        }
    }
}
