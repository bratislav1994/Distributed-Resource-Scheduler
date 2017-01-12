namespace ActivePowerGenerator
{
    using System;
    using System.ServiceModel;
    using CommonLibrary.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
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
