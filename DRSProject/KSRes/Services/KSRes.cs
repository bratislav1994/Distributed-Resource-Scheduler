using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using System.ServiceModel;
using CommonLibrary.Exceptions;

namespace KSRes.Services
{
    public class KSRes : IKSRes, IKSForClient
    {
        public static DynamicDataBase dynamicDataBase = new DynamicDataBase();

        public void Login(string username, string password)
        {
            OperationContext context = OperationContext.Current;
            string sessionID = context.Channel.SessionId;
            ILKRes service = context.GetCallbackChannel<ILKRes>();

            try
            {
                dynamicDataBase.Login(username, password, service, sessionID);
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
                dynamicDataBase.Registration(username, password);
            }
            catch (FaultException<IdentificationExeption> ex)
            {
                throw ex;
            }
        }

        public void SendMeasurement(Dictionary<string, double> measurments)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateInfo update)
        {
            OperationContext context = OperationContext.Current;
            string sessionID = context.Channel.SessionId;
            ILKRes service = context.GetCallbackChannel<ILKRes>();

            LKResService lkClient = dynamicDataBase.GetClient(sessionID);

            switch(update.UpdateType)
            {
                case UpdateType.ADD:
                    break;

                case UpdateType.REMOVE:
                    break;

                case UpdateType.UPDATE:
                    break;
            }
        }

        /// <summary>
        /// IKSForClient implementation
        /// </summary>
        /// <returns></returns>
        public List<LKResService> GetAllSystem()
        {
            OperationContext context = OperationContext.Current;
            ILKRes service = context.GetCallbackChannel<ILKRes>();

            return dynamicDataBase.GetAllSystem();
        }

        public void IssueCommand(string username, double requiredAP)
        {
            throw new NotImplementedException();
        }
    }
}
