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


            ServiceHost KSResHost = new ServiceHost(typeof(KSRes.Services.KSRes));
            KSResHost.Open();


            Console.WriteLine("Services are started...");
            Console.ReadKey();

           

            Console.ReadKey();
        }
    }
}
