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
        private BindingList<Generator> generators;
        private List<LKResService> users;
        private IKSForClient proxy = null;

        public Client()
        {
            generators = new BindingList<Generator>();
            users = new List<LKResService>();
            DuplexChannelFactory<IKSForClient> factory = new DuplexChannelFactory<IKSForClient>(
                    new InstanceContext(this),
                        new NetTcpBinding(),
                        new EndpointAddress("net.tcp://localhost:10020/IKSForClient"));
            proxy = factory.CreateChannel();

            users = proxy.GetAllSystem();
            foreach(LKResService user in users)
            {
                foreach(Site site in user.Sites)
                {
                    foreach(Group group in site.Groups)
                    {
                        foreach(Generator g in group.Generators)
                        {
                            generators.Add(g);
                        }
                    }
                }
            }
        }

        public BindingList<Generator> Generators
        {
            get
            {
                return generators;
            }
            set
            {
                generators = value;
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
