using CommonLibrary;
using CommonLibrary.Exceptions;
using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSRes
{
    public class DynamicDataBase
    {
        private Dictionary<string, string> registrationUsers = null;
        private List<LKResService> activeUsers = null;

        public DynamicDataBase()
        {
            registrationUsers = new Dictionary<string, string>();
            activeUsers = new List<LKResService>();

            Thread CheckIfLKClientIsAliveThread = new Thread(() => CheckIfLKClientIsAlive());
            CheckIfLKClientIsAliveThread.Start();
        }
        private bool CheckRegistrationUser(string username)
        {
            string regUser = null;

            if (registrationUsers.TryGetValue(username, out regUser))
            {
                return true;
            }
            return false;
        }

        public void Registration(string username, string password)
        {
            if (CheckRegistrationUser(username))
            {
                IdentificationExeption ex = new IdentificationExeption("User is exist.");
                throw new FaultException<IdentificationExeption>(ex);
            }

            registrationUsers.Add(username, password);
        }

        public void Login(string username, string password, ILKRes channel, string sessionID)
        {
            foreach (LKResService user in activeUsers)
            {
                if (user.Username.Equals(username))
                {
                    if (!registrationUsers[username].Equals(password))
                    {
                        IdentificationExeption ex = new IdentificationExeption("Password is not correct.");
                        throw new FaultException<IdentificationExeption>(ex);
                    }

                    LKResService newUser = new LKResService(username, channel, sessionID);
                }
            }
        }

        public void Logout(string username)
        {
            foreach (LKResService user in activeUsers)
            {
                if (user.Username.Equals(username))
                {
                    activeUsers.Remove(user);
                }
            }
        }

        public LKResService GetClient(string sessionID)
        {
            foreach(LKResService user in activeUsers)
            {
                if( user.SessionID.Equals(sessionID))
                {
                    return user;
                }
            }

            return null;
        }

        public List<LKResService> GetAllSystem()
        {
            return activeUsers;
        }

        private void CheckIfLKClientIsAlive()
        {
            List<LKResService> userForRemove = new List<LKResService>();

            foreach(LKResService user in activeUsers)
            {
                try
                {
                    user.Client.Ping();
                }
                catch
                {
                    userForRemove.Add(user);
                }
            }

            foreach(LKResService user in userForRemove)
            {
                activeUsers.Remove(user);
            }

            userForRemove.Clear();

            Thread.Sleep(1000);
        }
    }
}
