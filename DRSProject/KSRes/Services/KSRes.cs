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
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class KSRes : IKSRes, IKSForClient
    {
        private static DynamicDataBase dynamicDataBase = new DynamicDataBase();
       
        public static DynamicDataBase DynamicDataBase
        {
            get
            {
                return dynamicDataBase;
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
                DynamicDataBase.Login(username, password, service, sessionID);
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
                DynamicDataBase.Registration(username, password);
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

            string username = dynamicDataBase.GetServiceSID(sessionID).Username;

            dynamicDataBase.SendMeasurement(username, measurments);
        }

        public void Update(UpdateInfo update)
        {
            OperationContext context = OperationContext.Current;
            string sessionID = context.Channel.SessionId;

            try
            {
                DynamicDataBase.Update(sessionID, update);
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
            DynamicDataBase.AddClient(client);

            return DynamicDataBase.ActiveService;
        }

        public void IssueCommand(double requiredAP)
        {
            if (requiredAP < 0)
            {
                throw new ArgumentException();
            }

            LocalDB.Instance.AddConsuption(new Data.ConsuptionHistory()
            {
                Consuption = requiredAP
            });

            List<SetPoint> setPoints = P(requiredAP);

            if (setPoints.Count != 0)
            {
                foreach (LKResService service in dynamicDataBase.ActiveService)
                {
                    List<SetPoint> temp = new List<SetPoint>();
                    foreach (Generator generator in service.Generators)
                    {
                        SetPoint setPoint = setPoints.Where(x => x.GeneratorID.Equals(generator.MRID)).FirstOrDefault();

                        if (setPoint != null)
                        {
                            temp.Add(setPoint);
                        }
                    }
                    service.Client.SendSetPoint(temp);
                }
            }
        }
        #endregion IKSForClient
        

        private List<SetPoint> P(double requiredAP)
        {
            List<Generator> remoteGenerators = new List<Generator>();
            List<SetPoint> setPoints = new List<SetPoint>();
            List<Generator> generators = new List<Generator>();

            foreach(LKResService client in dynamicDataBase.ActiveService)
            {
                generators.AddRange(client.Generators);
            }

            double activePower = 0;
            double diff = 0;

            foreach (Generator generator in generators)
            {
                activePower += generator.ActivePower;

                if (generator.WorkingMode == WorkingMode.REMOTE)
                {
                    remoteGenerators.Add(generator);
                }
            }

            diff = requiredAP - activePower;

            if (diff > 0)
            {
                List<Generator> sortedList = remoteGenerators.OrderBy(o => o.Price).ToList();
                foreach (Generator remoteGenerator in sortedList)
                {
                    double diff1 = 0;
                    if ((diff1 = remoteGenerator.Pmax - remoteGenerator.ActivePower) > 0)
                    {
                        SetPoint setPoint = new SetPoint();

                        if (diff > diff1)
                        {
                            diff -= diff1;
                            setPoint.GeneratorID = remoteGenerator.MRID;
                            setPoint.Setpoint = remoteGenerator.Pmax;
                            setPoints.Add(setPoint);
                        }
                        else if (diff <= diff1)
                        {
                            setPoint.GeneratorID = remoteGenerator.MRID;
                            setPoint.Setpoint = remoteGenerator.ActivePower + diff;
                            diff = 0;
                            setPoints.Add(setPoint);
                            break;
                        }
                    }
                }
            }
            else
            {
                diff = Math.Abs(diff);
                List<Generator> sortedList = remoteGenerators.OrderByDescending(o => o.Price).ToList();
                foreach (Generator remoteGenerator in sortedList)
                {
                    if (remoteGenerator.ActivePower > 0)
                    {
                        SetPoint setPoint = new SetPoint();
                        if (diff != 0)
                        {
                            if (remoteGenerator.ActivePower <= diff)
                            {
                                setPoint.GeneratorID = remoteGenerator.MRID;
                                setPoint.Setpoint = 0;
                                diff -= remoteGenerator.ActivePower;
                                setPoints.Add(setPoint);
                            }
                            else
                            {
                                if ((remoteGenerator.ActivePower - remoteGenerator.Pmin) >= diff)
                                {
                                    setPoint.GeneratorID = remoteGenerator.MRID;
                                    setPoint.Setpoint = remoteGenerator.ActivePower - diff;
                                    diff = 0;
                                    setPoints.Add(setPoint);
                                    break;
                                }
                                else
                                {
                                    setPoint.GeneratorID = remoteGenerator.MRID;
                                    setPoint.Setpoint = remoteGenerator.Pmin;
                                    diff -= remoteGenerator.ActivePower - remoteGenerator.Pmin;
                                    setPoints.Add(setPoint);
                                }
                            }
                        }
                    }
                }
            }
            return setPoints;
        }
    }
}
