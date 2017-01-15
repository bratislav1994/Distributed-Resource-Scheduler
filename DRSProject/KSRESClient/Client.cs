//-----------------------------------------------------------------------
// <copyright file="Client.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRESClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using CommonLibrary;
    using CommonLibrary.Interfaces;
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Client : IKSClient
    {
        private BindingList<Generator> generatorsForShowing;
        private List<LKResService> allUsers;
        private IKSForClient proxy = null;
        private List<String> userNames;
        private LKResService currentUser;
        private object lockObj = new object();

        public Client()
        {
            generatorsForShowing = new BindingList<Generator>();
            BindingOperations.EnableCollectionSynchronization(generatorsForShowing, lockObj);
            allUsers = new List<LKResService>();
            UserNames = new List<string>();
            userNames.Add("All");
            currentUser = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public IKSForClient Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
                GetAllUser();
            }
        }

        public BindingList<Generator> Generators
        {
            get
            {
                return generatorsForShowing;
            }

            set
            {
                generatorsForShowing = value;
                RaisePropertyChanged("Generators");
            }
        }

        public List<string> UserNames
        {
            get
            {
                return userNames;
            }

            set
            {
                userNames = value;
                RaisePropertyChanged("Usernames");
            }
        }
        
        public void Update(UpdateInfo update, string username)
        {
            if (update == null)
            {
                throw new ArgumentException();
            }

            switch (update.UpdateType)
            {
                case UpdateType.ADD:
                    LKResService client = allUsers.Where(cln => cln.Username.Equals(username)).FirstOrDefault();
                    if (client == null)
                    {
                        client = new LKResService(username, null, null);
                        allUsers.Add(client);
                        userNames.Add(client.Username);
                    }

                    foreach (LKResService user in allUsers)
                    {
                        if (user.Username.Equals(username))
                        {
                            foreach (Generator g in update.Generators)
                            {
                                user.Generators.Add(g);
                            }

                            if (update.Groups != null)
                            {
                                foreach (Group group in update.Groups)
                                {
                                    user.Gropus.Add(group);
                                }
                            }

                            if (update.Sites != null)
                            {
                                foreach (Site s in update.Sites)
                                {
                                    user.Sites.Add(s);
                                }
                            }

                            FillListForShowing();
                        }
                    }

                    break;
                case UpdateType.REMOVE:
                    List<Generator> removingListGen = new List<Generator>();
                    List<Group> removingListGr = new List<Group>();
                    List<Site> removingListS = new List<Site>();
                    LKResService removeUser = GetUser(username);

                    if (removeUser != null)
                    {
                        foreach (Generator g in update.Generators)
                        {
                            foreach (Generator g1 in removeUser.Generators)
                            {
                                if (g.MRID.Equals(g1.MRID))
                                {
                                    removingListGen.Add(g1);
                                }
                            }

                            foreach (Generator g2 in removingListGen)
                            {
                                removeUser.Generators.Remove(g2);
                            }

                            removingListGen.Clear();
                        }

                        if (update.Groups != null)
                        {
                            foreach (Group g in update.Groups)
                            {
                                foreach (Group g1 in removeUser.Gropus)
                                {
                                    if (g.MRID.Equals(g1.MRID))
                                    {
                                        removingListGr.Add(g1);
                                    }
                                }

                                foreach (Group g2 in removingListGr)
                                {
                                    removeUser.Gropus.Remove(g2);
                                }

                                removingListGr.Clear();
                            }
                        }

                        if (update.Sites != null)
                        {
                            foreach (Site g in update.Sites)
                            {
                                foreach (Site g1 in removeUser.Sites)
                                {
                                    if (g.MRID.Equals(g1.MRID))
                                    {
                                        removingListS.Add(g1);
                                    }
                                }

                                foreach (Site g2 in removingListS)
                                {
                                    removeUser.Sites.Remove(g2);
                                }

                                removingListS.Clear();
                            }
                        }

                        FillListForShowing();
                    }

                    break;
                case UpdateType.UPDATE:
                    Dictionary<int, Generator> tempListGen = new Dictionary<int, Generator>();

                    LKResService updateUser = GetUser(username);

                    if (updateUser != null)
                    {
                        foreach (Generator g in update.Generators)
                        {
                            foreach (Generator g1 in updateUser.Generators)
                            {
                                if (g.MRID.Equals(g1.MRID))
                                {
                                    tempListGen.Add(updateUser.Generators.IndexOf(g1), g);
                                }
                            }
                        }

                        foreach (KeyValuePair<int, Generator> kp in tempListGen)
                        {
                            updateUser.Generators[kp.Key] = kp.Value;
                        }
                        
                        if (update.Groups != null)
                        {
                            foreach (Group group in update.Groups)
                            {
                                updateUser.Gropus.Add(group);
                            }
                        }

                        if (update.Sites != null)
                        {
                            foreach (Site s in update.Sites)
                            {
                                updateUser.Sites.Add(s);
                            }
                        }

                        FillListForShowing();
                    }

                    break;
            }
        }
        
        public void SetCurrentUser(string username)
        {
            if (username.Equals("All"))
            {
                currentUser = null;
            }
            else
            {
                foreach (LKResService user in allUsers)
                {
                    if (user.Username.Equals(username))
                    {
                        currentUser = user;
                        break;
                    }
                }
            }

            FillListForShowing();
        }

        public LKResService GetUser(string username)
        {
            LKResService user = null;
            try
            {
                user = allUsers.Where(x => x.Username.Equals(username)).First();
            }
            catch
            {
                user = null;
            }

            return user;
        }

        public void IssueCommand(double neededPower)
        {
            Proxy.IssueCommand(neededPower);
        }

        public Generator GetGeneratorFromId(string mrId)
        {
            foreach (LKResService user in allUsers)
            {
                foreach (Generator g in user.Generators)
                {
                    if (g.MRID.Equals(mrId))
                    {
                        return g;
                    }
                }
            }

            return null;
        }

        public String GetGroupNameFromId(String mrId)
        {
            foreach (LKResService user in allUsers)
            {
                foreach (Group g in user.Gropus)
                {
                    if (g.MRID.Equals(mrId))
                    {
                        return g.Name;
                    }
                }
            }

            return null;
        }

        public Group GetGroupFromId(String mrId)
        {
            foreach (LKResService user in allUsers)
            {
                foreach (Group g in user.Gropus)
                {
                    if (g.MRID.Equals(mrId))
                    {
                        return g;
                    }
                }
            }

            return null;
        }

        public String GetSiteNameFromId(String mrId)
        {
            foreach (LKResService user in allUsers)
            {
                foreach (Site s in user.Sites)
                {
                    if (s.MRID.Equals(mrId))
                    {
                        return s.Name;
                    }
                }
            }

            return null;
        }

        public SortedDictionary<DateTime, double> GetProductionHistory(double np)
        {
            return proxy.GetProductionHistory(np);
        }

        public SortedDictionary<DateTime, double> GetLoadForecast()
        {
            SortedDictionary<DateTime, double> retVal = new SortedDictionary<DateTime, double>();
            retVal = Proxy.GetLoadForecast();

            if (retVal == null)
            {
                throw new Exception("Not enaugh data.");
            }

            return Proxy.GetLoadForecast();
        }

        public void LoadForecastOnDemand()
        {
            Proxy.LoadForecastOnDemand();
        }

        public void DeleteService(string username)
        {
            LKResService user = allUsers.Where(o => o.Username.Equals(username)).FirstOrDefault();
            allUsers.Remove(user);

            userNames.Remove(username);
            FillListForShowing();
        }

        private void FillListForShowing()
        {
            lock (lockObj)
            {
                generatorsForShowing.Clear();
                if (currentUser == null)
                {
                    foreach (LKResService user in allUsers)
                    {
                        foreach (Generator g in user.Generators)
                        {
                            generatorsForShowing.Add(g);
                        }
                    }
                }
                else
                {
                    foreach (Generator g in currentUser.Generators)
                    {
                        generatorsForShowing.Add(g);
                    }
                }
            }
        }

        private void GetAllUser()
        {
            allUsers = Proxy.GetAllSystem();
            foreach (LKResService user in allUsers)
            {
                userNames.Add(user.Username);
            }

            FillListForShowing();
        }

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
