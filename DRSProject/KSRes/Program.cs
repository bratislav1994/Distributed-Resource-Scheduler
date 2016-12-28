using CommonLibrary.Interfaces;
using KSRes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KSRes
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILKRes> factory = new ChannelFactory<ILKRes>("ClientLSRes");
            ILKRes proxy = factory.CreateChannel();

            ServiceHost KSResHost = new ServiceHost(typeof(KSForClient));
            KSResHost.Open();

            ServiceHost KSForClientHost = new ServiceHost(typeof(KSForClient));
            KSForClientHost.Open();

            Console.WriteLine("Services are started...");
            Console.ReadKey();

            Console.WriteLine(proxy.Ping());

            Console.ReadKey();
        }
    }
}
