using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LoadForecast
{
    public class Program
    {
        private static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:10040/ILoadForecast";
            ServiceHost host = new ServiceHost(typeof(LoadForecastService));
            host.AddServiceEndpoint(typeof(ILoadForecast), binding, address);
            host.Open();

            Console.WriteLine("Service started, press <enter> to close...");

            Console.ReadKey();

            host.Close();
        }
    }
}
