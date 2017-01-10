using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using System.ServiceModel;
using CommonLibrary.Exceptions;
using KSRes.Access;

namespace KSRes.Services
{
    //[CallbackBehavior(UseSynchronizationContext = false)]
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

            string username = Controler.GetServiceSID(sessionID).Username;

            Controler.SendMeasurement(username, measurments);
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

            List<SetPoint> setPoints = Controler.P(requiredAP);
            Controler.DeploySetPoint(setPoints);
        }

        public SortedDictionary<DateTime, double> GetLoadForecast()
        {
            if(Controler.LastValuesLC.Count != 0)
            {
                return Controler.LastValuesLC;
            }
            return null;
        }
        #endregion IKSForClient
    }
}
