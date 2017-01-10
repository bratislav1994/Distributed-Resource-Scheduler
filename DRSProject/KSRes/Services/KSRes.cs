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
using KSRes.Data;

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

            List<Point> setPoints = Controler.P(requiredAP, false);
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

        public SortedDictionary<DateTime, double> GetProductionHistory(double days)
        {
            SortedDictionary<DateTime, double> retVal = new SortedDictionary<DateTime, double>();
            DateTime condition = DateTime.Now.AddMinutes(0 - days);
            Dictionary<DateTime, List<double>> temp = new Dictionary<DateTime, List<double>>();
            List<ProductionHistory> productions = LocalDB.Instance.ReadProductions(condition);

            foreach(ProductionHistory production in productions)
            {
                if (!temp.ContainsKey(production.TimeStamp))
                {
                    temp[production.TimeStamp] = new List<double>();
                }
                temp[production.TimeStamp].Add(production.ActivePower);
            }
            
            foreach(KeyValuePair<DateTime,List<double>> kp in temp)
            {
                retVal.Add(kp.Key, SumActivePower(kp.Value));
            }

            return retVal;
        }

        private double SumActivePower(List<double> list)
        {
            double retVal = 0;
            if (list != null)
            {
                foreach (double d in list)
                {
                    retVal += d;
                }
            }
            return retVal;
        }
        #endregion IKSForClient
    }
}
