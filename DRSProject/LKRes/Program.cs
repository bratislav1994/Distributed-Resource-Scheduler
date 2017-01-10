// <copyright file="Program.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace LKRes
{
    using System;
    using System.ServiceModel;
    using CommonLibrary.Interfaces;

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
