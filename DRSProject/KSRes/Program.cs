//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using CommonLibrary.Interfaces;
    using KSRes.Access;
    using KSRes.Services;

    public class Program
    {
        private static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:10020/IKSForClient";
            ServiceHost host = new ServiceHost(typeof(Services.KSRes));
            host.AddServiceEndpoint(typeof(IKSForClient), binding, address);
            host.Open();

            NetTcpBinding binding1 = new NetTcpBinding();
            string address1 = "net.tcp://localhost:10010/IKSRes";
            ServiceHost host1 = new ServiceHost(typeof(Services.KSRes));
            host1.AddServiceEndpoint(typeof(IKSRes), binding1, address1);
            host1.Open();

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("KSRes"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path + "KSRes");

            // update database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

            Console.WriteLine("Services are started...");
            Console.ReadKey();
        }
    }
}
