// <copyright file="Program.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace LKRes
{
    using CommonLibrary.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using Services;

    /// <summary>
    /// Program.cs of LKRes server
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method of class Program.cs
        /// </summary>
        /// <param name="args">Command-Line arguments</param>
        public static void Main(string[] args)
        {
            LKForClientService instance = new LKForClientService();
            
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:4000/ILKRes";
            ServiceHost host = new ServiceHost(instance);
            host.AddServiceEndpoint(typeof(ILKRes), binding, address);
            host.Open();

            NetTcpBinding binding1 = new NetTcpBinding();
            string address1 = "net.tcp://localhost:5000/ILKForClient";
            ServiceHost host1 = new ServiceHost(instance);
            host1.AddServiceEndpoint(typeof(ILKForClient), binding1, address1);
            host1.Open();

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("bin"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            // update database
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<Access.AccessDB, Access.Configuration>());

            Console.WriteLine("Services are started...");
            Console.ReadKey();
        }
    }
}
