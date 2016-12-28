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
        private static UpdateInfo updateInfo = new UpdateInfo();
        public UpdateInfo GetMySystem()
        {
            return updateInfo;
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

        public void Update(UpdateInfo update)
        {
            switch (update.UpdateType)
            {
                case UpdateType.ADD:
                    Add(update);
                    break;
                case UpdateType.REMOVE:
                    Remove(update);
                    break;
                case UpdateType.UPDATE:
                    UpdateData(update);
                break;
            }
        }

        public void Add(UpdateInfo update)
        {
            if (updateInfo.Generators != null && updateInfo.Sites != null && updateInfo.Groups != null)
            {
                updateInfo.Generators.Add(update.Generators[0]);
                updateInfo.Sites.Add(update.Sites[0]);
                updateInfo.Groups.Add(update.Groups[0]);
            }
        }

        public void Remove(UpdateInfo update)
        { 
            Generator gen = updateInfo.Generators.Where(mrID => mrID.Equals(update.Generators[0].MRID)).FirstOrDefault();
            updateInfo.Generators.Remove(gen);

            Group group = null;
            bool areEquals = false;
            foreach (Group gIterator in updateInfo.Groups)
            {
                if (gIterator.MRID.Equals(update.Generators[0].GroupID))
                {
                    areEquals = true;
                    break;
                }  
            }

            if (!areEquals)
            {
                updateInfo.Groups.Remove(update.Groups[0]);
            }
            else
            {
                areEquals = false;

            }

            foreach (Site sIterator in updateInfo.Sites)
            {
                if (sIterator.MRID.Equals(update.Groups[0].SiteID))
                {
                    areEquals = true;
                    break;
                }
            }

            if (!areEquals)
            {
                updateInfo.Sites.Remove(update.Sites[0]);
            }
        }

        public void UpdateData(UpdateInfo update)
        {
            
        }
    }
}
