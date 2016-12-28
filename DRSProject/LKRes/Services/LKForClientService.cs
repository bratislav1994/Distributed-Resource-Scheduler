using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary.Interfaces;
using CommonLibrary;
using CommonLibrary.Exceptions;
using System.ServiceModel;

namespace LKRes.Services
{
    public class LKForClientService : ILKForClient
    {
        private static ChannelFactory<IKSRes> ksResFactory = new ChannelFactory<IKSRes>("ClientKSRes");
        private static IKSRes kSResProxy = ksResFactory.CreateChannel();

        public List<Site> GetMySystem()
        {
            throw new NotImplementedException();
        }

        public void Login(string username, string password)
        {
            try
            {
                kSResProxy.Login(username, password);
            }
            catch (FaultException<IdentificationExeption> ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

        public void Registration(string username, string password)
        {
            try
            {
                kSResProxy.Registration(username, password);
            }
            catch (FaultException<IdentificationExeption> ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            
        }
    }
}
