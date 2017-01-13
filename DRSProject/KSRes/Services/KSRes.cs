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
            string sessionID = context.Channel.SessionId;
            ILKRes service = context.GetCallbackChannel<ILKRes>();

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

            Controler.SendMeasurement(service.Username, measurments);
        }

        public void Update(UpdateInfo update)
        {
            OperationContext context = OperationContext.Current;
            string sessionID = context.Channel.SessionId;

            try
            {
                Controler.Update(sessionID, update);
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
            IKSClient client = context.GetCallbackChannel<IKSClient>();

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

            List<Point> setPoints = Controler.P(requiredAP, false);
            Controler.DeploySetPoint(setPoints);
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

        #endregion IKSForClient
    }
}
