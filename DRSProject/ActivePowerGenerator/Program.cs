using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CommonLibrary.Interfaces;

namespace ActivePowerGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:3030/IActivePowerManagement";
            ServiceHost host = new ServiceHost(typeof(ActivePowerManagement));
            host.AddServiceEndpoint(typeof(IActivePowerManagement), binding, address);
            host.Open();

            Console.WriteLine("Service is started...");
            Console.ReadKey();
        }
    }
}
