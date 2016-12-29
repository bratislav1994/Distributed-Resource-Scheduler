using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Interfaces;
using System.ComponentModel;
using System.ServiceModel;

namespace KLRESClient
{
    public class LKClientService : ILKClient
    {
        private UpdateInfo getAllFromService;
        private ILKForClient proxy = null;
        private BindingList<Generator> generators;
        private BindingList<Site> sites;
        private BindingList<Group> groups;
        private List<bool> hasMeasurments;
        private List<GeneratorType> generatorTypes;
        private List<WorkingMode> workingModes;

        public LKClientService()
        {
            generators = new BindingList<Generator>();
            sites = new BindingList<Site>();
            groups = new BindingList<Group>();
            hasMeasurments = new List<bool>() { true, false };
            generatorTypes = new List<GeneratorType>();
            workingModes = new List<WorkingMode>();

            foreach (GeneratorType genType in Enum.GetValues(typeof(GeneratorType)))
            {
                generatorTypes.Add(genType);
            }

            foreach (WorkingMode workMode in Enum.GetValues(typeof(WorkingMode)))
            {
                workingModes.Add(workMode);
            }

            DuplexChannelFactory<ILKForClient> factory = new DuplexChannelFactory<ILKForClient>(
                    new InstanceContext(this),
                        new NetTcpBinding(),
                        new EndpointAddress("net.tcp://localhost:5000/ILKForClient"));
            proxy = factory.CreateChannel();

            getAllFromService = proxy.GetMySystem(); 
        }

        
        public List<bool> HasMeasurments
        {
            get
            {
                return hasMeasurments;
            }
            set
            {
                hasMeasurments = value;
            }
        }

        public List<GeneratorType> GeneratorTypes
        {
            get
            {
                return generatorTypes;
            }
            set
            {
                generatorTypes = value;
            }
        }

        public List<WorkingMode> WorkingModes
        {
            get
            {
                return workingModes;
            }
            set
            {
                workingModes = value;
            }
        }

        public void Command(UpdateInfo update)
        {
            proxy.Update(update);
        }

        public void Update(UpdateInfo update)
        {
            switch (update.UpdateType)
            {
                case UpdateType.ADD:
                    if (update.Generators != null)
                    {
                        Generators.Add(update.Generators[0]);
                    }
                    if (update.Groups != null)
                    {
                        Groups.Add(update.Groups[0]);
                    }
                    if (update.Sites != null)
                    {
                        Sites.Add(update.Sites[0]);
                    }
                    break;
                case UpdateType.REMOVE:
                    if (update.Generators != null)
                    {
                        Generators.Remove(update.Generators[0]);
                    }
                    if (update.Groups != null)
                    {
                        Groups.Remove(update.Groups[0]);
                    }
                    if (update.Sites != null)
                    {
                        Sites.Remove(update.Sites[0]);
                    }
                    break;
                case UpdateType.UPDATE:
                    if (update.Generators != null)
                    {
                        Generator gen = Generators.SingleOrDefault(p => p.MRID == update.Generators[0].MRID);
                        if (gen != null)
                        {
                            gen = update.Generators[0];
                        }
                    }
                    if (update.Groups != null)
                    {
                        Group group = Groups.SingleOrDefault(p => p.MRID == update.Groups[0].MRID);
                        if (group != null)
                        {
                            group = update.Groups[0];
                        }
                    }
                    if (update.Sites != null)
                    {
                        Site site = Sites.SingleOrDefault(p => p.MRID == update.Sites[0].MRID);
                        if (site != null)
                        {
                            site = update.Sites[0];
                        }
                    }
                    break;
            }
        }

        public BindingList<Generator> Generators
        {
            get
            {
                return generators;
            }
            set
            {
                generators = value;
            }
        }

        public BindingList<Site> Sites
        {
            get
            {
                return sites;
            }
            set
            {
                sites = value;
            }
        }

        public BindingList<Group> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
            }
        }

        public string GetGroupNameFromId(string mrId)
        {
            Group group = Groups.SingleOrDefault(p => p.MRID == mrId);
            if (group != null)
            {
                return group.Name;
            }

            return null;
        }

        public Group GetGroupFromId(string mrId)
        {
            Group group = Groups.SingleOrDefault(p => p.MRID == mrId);
            if (group != null)
            {
                return group;
            }

            return null;
        }

        public string GetSiteNameFromId(string mrId)
        {
            Site site = Sites.SingleOrDefault(p => p.MRID == mrId);
            if (site != null)
            {
                return site.Name;
            }

            return null;
        }

        public Site GetSiteFromId(string mrId)
        {
            Site site = Sites.SingleOrDefault(p => p.MRID == mrId);
            if (site != null)
            {
                return site;
            }

            return null;
        }
    }
}
