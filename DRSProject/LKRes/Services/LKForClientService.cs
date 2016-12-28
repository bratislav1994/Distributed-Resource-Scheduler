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
        private static UpdateInfo updateInfo = new UpdateInfo();
        private IKSRes kSResProxy = null;

        public LKForClientService()
        {
            DuplexChannelFactory<IKSRes> ksResFactory = new DuplexChannelFactory<IKSRes>(
                new InstanceContext(this),
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:10010/IKSRes"));

            kSResProxy = ksResFactory.CreateChannel();
        }

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
                update.Generators[0].MRID = Guid.NewGuid().ToString();
                update.Groups[0].MRID = Guid.NewGuid().ToString();
                update.Sites[0].MRID = Guid.NewGuid().ToString();

                update.Generators[0].GroupID = update.Groups[0].MRID;
                update.Groups[0].SiteID = update.Groups[0].MRID;
               
                updateInfo.Generators.Add(update.Generators[0]);
                updateInfo.Sites.Add(update.Sites[0]);
                updateInfo.Groups.Add(update.Groups[0]);
            }
        }

        public void Remove(UpdateInfo update)
        {
            Generator gen = null;
            gen = updateInfo.Generators.Where(mrID => mrID.Equals(update.Generators[0].MRID)).FirstOrDefault();

            if(gen != null)
                updateInfo.Generators.Remove(gen);

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
            Generator generator = updateInfo.Generators.Where(mrID => mrID.Equals(update.Generators[0].MRID)).FirstOrDefault();

            if (generator != null)
            {
                generator.ActivePower = update.Generators[0].ActivePower;
                generator.BasePoint = update.Generators[0].BasePoint;
                generator.GeneratorType = update.Generators[0].GeneratorType;
                generator.GroupID = update.Generators[0].GroupID;
                generator.HasMeasurment = update.Generators[0].HasMeasurment;
                generator.Name = update.Generators[0].Name;
                generator.Pmax = update.Generators[0].Pmax;
                generator.Pmin = update.Generators[0].Pmin;
                generator.Price = update.Generators[0].Price;
                generator.SetPoint = update.Generators[0].SetPoint;
                generator.WorkingMode = update.Generators[0].WorkingMode;
            }

            Group group = updateInfo.Groups.Where(mrID => mrID.Equals(update.Groups[0].MRID)).FirstOrDefault();

            if (group != null)
            {
                group.Name = updateInfo.Groups[0].Name;
                group.SiteID = updateInfo.Groups[0].SiteID;
            }

            Site site = updateInfo.Sites.Where(mrID => mrID.Equals(update.Sites[0].MRID)).FirstOrDefault();

            if (site != null)
            {
                site.Name = updateInfo.Sites[0].Name;
            }
        }
    }
}
