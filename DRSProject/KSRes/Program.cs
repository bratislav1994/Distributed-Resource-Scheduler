using CommonLibrary.Interfaces;
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

            Console.WriteLine(proxy.Ping());

            Console.ReadKey();
        }
    }
}
