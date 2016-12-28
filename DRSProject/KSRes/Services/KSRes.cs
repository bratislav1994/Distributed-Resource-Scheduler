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
            catch
            {

            }
        }
        #endregion ILKRes

        #region IKSForClient
        public List<LKResService> GetAllSystem()
        {
            return dynamicDataBase.GetAllSystem();
        }

        public void IssueCommand(string username, double requiredAP)
        {
            List<Generator> remoteGenerators = new List<Generator>();
            List<SetPoint> setPoints = new List<SetPoint>();
            LKResService user = dynamicDataBase.GetUser(username);
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
                        }
                        else if (diff <= diff1)
                        {
                            setPoint.GeneratorID = remoteGenerator.MRID;
                            setPoint.Setpoint = remoteGenerator.ActivePower + diff;
                            diff = 0;
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
                            }
                            else
                            {
                                if ((remoteGenerator.ActivePower - remoteGenerator.Pmin) >= diff)
                                {
                                    setPoint.GeneratorID = remoteGenerator.MRID;
                                    setPoint.Setpoint = remoteGenerator.ActivePower - diff;
                                    diff = 0;
                                    break;
                                }
                                else
                                {
                                    setPoint.GeneratorID = remoteGenerator.MRID;
                                    setPoint.Setpoint = remoteGenerator.Pmin;
                                    diff -= remoteGenerator.Pmin;
                                }
                            }
                        }
                    }
                }
            }

        }

        #endregion IKSForClient
    }
}
