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
            ServiceHost LKResHost = new ServiceHost(typeof(LKResService));
            LKResHost.Open();

            ServiceHost LKForClientHost = new ServiceHost(typeof(LKForClientService));
            LKForClientHost.Open();

            Console.WriteLine("Services are started...");
            Console.ReadKey();
        }
    }
}
