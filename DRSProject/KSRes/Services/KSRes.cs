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
    public class KSRes : IKSRes, IKSForClient
    {
        public static DynamicDataBase dynamicDataBase = new DynamicDataBase();

        #region ILKRes
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

            try
            {
                dynamicDataBase.Update(sessionID, update);
            }
            catch(Exception ex)
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
            dynamicDataBase.AddClient(client);

            return dynamicDataBase.ActiveService;
        }

        public void IssueCommand(string username, double requiredAP)
        {
            LocalDB.Instance.AddConsuption(new Data.ConsuptionHistory() { Username = username, Consuption = requiredAP });
             
            List<Generator> remoteGenerators = new List<Generator>();
            List<SetPoint> setPoints = new List<SetPoint>();
            LKResService user = dynamicDataBase.GetService(username);
            double activePower = 0;
            double diff = 0;

            foreach(Generator generator in user.Generators)
            {
                activePower += generator.ActivePower;
            }

            diff = requiredAP - activePower;

            foreach (Generator generator in user.Generators)
            {
                if (generator.WorkingMode == WorkingMode.REMOTE)
                {
                    remoteGenerators.Add(generator);
                }
            }
            
            if (diff > 0)
            {
                List<Generator> sortedList = remoteGenerators.OrderBy(o => o.Price).ToList();
                foreach (Generator remoteGenerator in remoteGenerators)
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
                foreach (Generator remoteGenerator in remoteGenerators)
                {
                    if ( remoteGenerator.ActivePower > 0 )
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
                                    diff -= remoteGenerator.Pmin;
                                    setPoints.Add(setPoint);
                                }
                            }
                        }
                    }
                }
            }

            dynamicDataBase.GetService(username).Client.SendSetPoint(setPoints);
        }

        #endregion IKSForClient
    }
}
