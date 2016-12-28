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
             foreach(LKResService user in allUsers)
             {
                 foreach (Generator g in user.Generators)
                 {
                     generatorsForShowing.Add(g);
                 }
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


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        
        public void Update(UpdateInfo update, string username)
        {
            throw new NotImplementedException();
        }
    }
}
