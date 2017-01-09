using CommonLibrary.Interfaces;
using LKRes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LKRes
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:4000/ILKRes";
            ServiceHost host = new ServiceHost(typeof(Services.LKForClientService));
            host.AddServiceEndpoint(typeof(ILKRes), binding, address);
            host.Open();

            NetTcpBinding binding1 = new NetTcpBinding();
            string address1 = "net.tcp://localhost:5000/ILKForClient";
            ServiceHost host1 = new ServiceHost(typeof(Services.LKForClientService));
            host1.AddServiceEndpoint(typeof(ILKForClient), binding1, address1);
            host1.Open();

            Console.WriteLine("Services are started...");
            Console.ReadKey();
        }
    }
}
