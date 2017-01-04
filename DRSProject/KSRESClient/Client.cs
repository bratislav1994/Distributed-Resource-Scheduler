using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using System.ComponentModel;
using System.ServiceModel;

namespace KSRESClient
{
    public class Client : IKSClient
    {
        private BindingList<Generator> generatorsForShowing;
        private List<LKResService> allUsers;
        private IKSForClient proxy = null;
        private List<String> userNames;
        private LKResService currentUser;

        public Client()
        {
            generatorsForShowing = new BindingList<Generator>();
            allUsers = new List<LKResService>();
            UserNames = new List<string>();
            userNames.Add("All");
            currentUser = null;

            DuplexChannelFactory<IKSForClient> factory = new DuplexChannelFactory<IKSForClient>(
                    new InstanceContext(this),
                        new NetTcpBinding(),
                        new EndpointAddress("net.tcp://localhost:10020/IKSForClient"));
            proxy = factory.CreateChannel();

            allUsers = proxy.GetAllSystem();
            foreach(LKResService user in allUsers)
            {
                userNames.Add(user.Username);
            }
            FillListForShowing();
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
            if (update == null || update.Generators == null ||
                update.Groups == null || update.Sites == null) 
            {
                throw new ArgumentException();
            }   
            switch(update.UpdateType)
            {
                case UpdateType.ADD:
                    LKResService client = allUsers.Where(cln => cln.Username.Equals(username)).FirstOrDefault();
                    if(client == null)
                    {
                        client = new LKResService(username, null, null);
                        allUsers.Add(client);
                        userNames.Add(client.Username);
                    }
                    foreach(LKResService user in allUsers)
                    {
                        if(user.Username.Equals(username))
                        {
                            foreach(Generator g in update.Generators)
                            {
                                user.Generators.Add(g);
                            }
                            foreach(Group group in update.Groups)
                            {
                                user.Gropus.Add(group);
                            }
                            foreach(Site s in update.Sites)
                            {
                                user.Sites.Add(s);
                            }
                            FillListForShowing();
                            break;
                        }
                    }
                    break;
                case UpdateType.REMOVE:
                    List<Generator> removingListGen = new List<Generator>();
                    List<Group> removingListGr = new List<Group>();
                    List<Site> removingListS = new List<Site>();
                    foreach (LKResService user in allUsers)
                    {
                        if (user.Username.Equals(username))
                        {
                            foreach(Generator g in update.Generators)
                            {
                                foreach (Generator g1 in user.Generators)
                                {
                                    if (g.MRID.Equals(g1.MRID))
                                    {
                                        removingListGen.Add(g1);
                                    }
                                }
                                foreach(Generator g2 in removingListGen)
                                {
                                    user.Generators.Remove(g2);
                                }
                                removingListGen.Clear();
                            }
                            foreach (Group g in update.Groups)
                            {
                                foreach (Group g1 in user.Gropus)
                                {
                                    if (g.MRID.Equals(g1.MRID))
                                    {
                                        removingListGr.Add(g1);
                                    }
                                }
                                foreach (Group g2 in removingListGr)
                                {
                                    user.Gropus.Remove(g2);
                                }
                                removingListGr.Clear();
                            }
                            foreach (Site g in update.Sites)
                            {
                                foreach (Site g1 in user.Sites)
                                {
                                    if (g.MRID.Equals(g1.MRID))
                                    {
                                        removingListS.Add(g1);
                                    }
                                }
                                foreach (Site g2 in removingListS)
                                {
                                    user.Sites.Remove(g2);
                                }
                                removingListS.Clear();
                            }
                            FillListForShowing();
                            break;
                        }
                    }
                    break;
                case UpdateType.UPDATE:
                    Dictionary<int, Generator> tempListGen = new Dictionary<int, Generator>();
                    Dictionary<int, Group> tempLisrGr = new Dictionary<int, Group>();
                    Dictionary<int, Site> tempLisrS = new Dictionary<int, Site>();
                    foreach (LKResService user in allUsers)
                    {
                        if (user.Username.Equals(username))
                        {
                            foreach(Generator g in update.Generators)
                            {
                                foreach(Generator g1 in user.Generators)
                                {
                                    if(g.MRID.Equals(g1.MRID))
                                    {
                                        tempListGen.Add(user.Generators.IndexOf(g1), g1);
                                    }
                                }
                            }
                            foreach(KeyValuePair<int,Generator> kp in tempListGen)
                            {
                                user.Generators[kp.Key] = kp.Value;
                            }
                            foreach (Group g in update.Groups)
                            {
                                foreach (Group g1 in user.Gropus)
                                {
                                    if (g.MRID.Equals(g1.MRID))
                                    {
                                        tempLisrGr.Add(user.Gropus.IndexOf(g1), g1);
                                    }
                                }
                            }
                            foreach (KeyValuePair<int, Group> kp in tempLisrGr)
                            {
                                user.Gropus[kp.Key] = kp.Value;
                            }
                            foreach (Site g in update.Sites)
                            {
                                foreach (Site g1 in user.Sites)
                                {
                                    if (g.MRID.Equals(g1.MRID))
                                    {
                                        tempLisrS.Add(user.Sites.IndexOf(g1), g1);
                                    }
                                }
                            }
                            foreach (KeyValuePair<int, Site> kp in tempLisrS)
                            {
                                user.Sites[kp.Key] = kp.Value;
                            }
                            FillListForShowing();
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void FillListForShowing()
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
                foreach(Generator g in currentUser.Generators)
                {
                    generatorsForShowing.Add(g);
                }
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

        public void IssueCommand(string userName, double neededPower)
        {
            proxy.IssueCommand(userName, neededPower);
        }

        public Generator GetGeneratorFromId(string mrId)
        {
            foreach(LKResService user in allUsers)
            {
                foreach(Generator g in user.Generators)
                {
                    if(g.MRID.Equals(mrId))
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
