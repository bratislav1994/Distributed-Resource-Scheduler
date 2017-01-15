//-----------------------------------------------------------------------
// <copyright file="KSRes.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using Access;
    using CommonLibrary;
    using CommonLibrary.Exceptions;
    using CommonLibrary.Interfaces;
    using Data;
    using System.ServiceModel.Channels;
    using System.Threading;

    public class KSRes : IKSRes, IKSForClient
    {
        private static Controler controler = new Controler();
       
        public static Controler Controler
        {
            get
            {
                return controler;
            }
        }

        #region ILKRes
        public void Login(string username, string password)
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            if(ip.Equals("::1"))
            {
                ip = "localhost";
            }

            ChannelFactory<ILKRes> factory = new ChannelFactory<ILKRes>(
                       new NetTcpBinding(),
                       new EndpointAddress("net.tcp://"+ ip +":4000/ILKRes"));
            string sessionID = context.Channel.SessionId;

            ILKRes service = factory.CreateChannel();

            try
            {
                Controler.Login(username, password, service, sessionID);
            }
            catch (FaultException<IdentificationExeption> ex)
            {
                throw ex;
            }
        }

        public void Registration(string username, string password)
        {
            try
            {
                Controler.Registration(username, password);
            }
            catch (FaultException<IdentificationExeption> ex)
            {
                throw ex;
            }
        }

        public void SendMeasurement(Dictionary<string, double> measurments)
        {
            OperationContext context = OperationContext.Current;
            string sessionID = context.Channel.SessionId;

            LKResService service = null;

            if( (service = Controler.GetServiceSID(sessionID) ) == null)
            {
                IdentificationExeption ex = new IdentificationExeption("Service not logged in.");
                throw new FaultException<IdentificationExeption>(ex);
            }

            new Thread(() => Controler.SendMeasurement(service.Username, measurments)).Start();
        }

        public void Update(UpdateInfo update)
        {
            OperationContext context = OperationContext.Current;
            string sessionID = context.Channel.SessionId;

            try
            {
                new Thread(() => Controler.Update(sessionID, update)).Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion ILKRes

        #region IKSForClient
        public List<LKResService> GetAllSystem()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            if (ip.Equals("::1"))
            {
                ip = "localhost";
            }

            ChannelFactory<IKSClient> factory = new ChannelFactory<IKSClient>(
                       new NetTcpBinding(),
                       new EndpointAddress("net.tcp://" + ip + ":10030/IKSClient"));
            string sessionID = context.Channel.SessionId;

            IKSClient client = factory.CreateChannel();

            Controler.Clients.Add(client);

            return Controler.ActiveService;
        }

        public void IssueCommand(double requiredAP)
        {
            if (requiredAP < 0)
            {
                throw new ArgumentException();
            }

            LocalDB.Instance.AddConsuption(new Data.ConsuptionHistory()
            {
                Consuption = requiredAP,
                TimeStamp = DateTime.Now
            });

            new Thread(() =>
            {
                List<Point> setPoints = Controler.P(requiredAP, false);
                Controler.DeploySetPoint(setPoints);
            }).Start();
        }

        public SortedDictionary<DateTime, double> GetLoadForecast()
        {
            if (Controler.LastValuesLC.Count != 0)
            {
                return Controler.LastValuesLC;
            }

            return null;
        }

        public SortedDictionary<DateTime, double> GetProductionHistory(double days)
        {
            if (days < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return controler.GetProductionHistory(days);
        }

        public void LoadForecastOnDemand()
        {
            controler.LoadForecast();
        }

        #endregion IKSForClient
    }
}
