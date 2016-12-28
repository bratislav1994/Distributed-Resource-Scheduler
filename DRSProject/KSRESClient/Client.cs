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

        public Client()
        {
            generatorsForShowing = new BindingList<Generator>();
            allUsers = new List<LKResService>();
                DuplexChannelFactory<IKSForClient> factory = new DuplexChannelFactory<IKSForClient>(
                        new InstanceContext(this),
                            new NetTcpBinding(),
                            new EndpointAddress("net.tcp://localhost:10020/IKSForClient"));
                proxy = factory.CreateChannel();

                allUsers = proxy.GetAllSystem();
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
        
        
        public void Update(UpdateInfo update, string username)
        {
            
            switch(update.UpdateType)
            {
                case UpdateType.ADD:
                    foreach(LKResService user in allUsers)
                    {
                        if(user.Username.Equals(username))
                        {
                            foreach(Generator g in update.Generators)
                            {
                                user.Generators.Add(g);
                            }
                            FillListForShowing();
                            break;
                        }
                    }
                    break;
                case UpdateType.REMOVE:
                    List<Generator> removingList = new List<Generator>();
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
                                        removingList.Add(g1);
                                    }
                                }
                                foreach(Generator g2 in removingList)
                                {
                                    user.Generators.Remove(g2);
                                }
                                removingList.Clear();
                            }
                            FillListForShowing();
                            break;
                        }
                    }
                    break;
                case UpdateType.UPDATE:
                    Dictionary<int, Generator> tempList = new Dictionary<int, Generator>();
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
                                        tempList.Add(user.Generators.IndexOf(g1), g1);
                                    }
                                }
                            }
                            foreach(KeyValuePair<int,Generator> kp in tempList)
                            {
                                user.Generators[kp.Key] = kp.Value;
                            }
                            FillListForShowing();
                            break;
                        }
                    }
                    break;
            }
        }

        public void FillListForShowing()
        {
            generatorsForShowing.Clear();
            foreach (LKResService user in allUsers)
            {
                foreach (Generator g in user.Generators)
                {
                    generatorsForShowing.Add(g);
                }
            }
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
