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
        private BindingList<bool> hasMeasurments;
        private List<GeneratorType> generatorTypes;
        private List<WorkingMode> workingModes;

        private BindingList<Site> siteNames;
        private BindingList<Group> groupNames;
        private BindingList<Site> cmb3SiteNames;

        public LKClientService()
        {
            generators = new BindingList<Generator>();
            sites = new BindingList<Site>();
            groups = new BindingList<Group>();
            hasMeasurments = new BindingList<bool>() { true, false };
            generatorTypes = new List<GeneratorType>();
            workingModes = new List<WorkingMode>();

            siteNames = new BindingList<Site>();
            groupNames = new BindingList<Group>();
            cmb3SiteNames = new BindingList<Site>();

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

            if (getAllFromService != null)
            {
                if (getAllFromService.Generators != null && getAllFromService.Generators.Count != 0)
                {
                    getAllFromService.Generators.ForEach(x => { Generators.Add(x); });
                }
                if (getAllFromService.Sites != null && getAllFromService.Sites.Count != 0)
                {
                    getAllFromService.Sites.ForEach(x => { Sites.Add(x); });
                }
                if (getAllFromService.Groups != null && getAllFromService.Groups.Count != 0)
                {
                    getAllFromService.Groups.ForEach(x => { Groups.Add(x); });
                }
            }
        }

        
        public BindingList<bool> HasMeasurments
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
                RaisePropertyChanged("Generators");
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

        public BindingList<Site> SiteNames
        {
            get
            {
                return SiteNames;
            }

            set
            {
                SiteNames = value;
            }
        }

        public BindingList<Group> GroupNames
        {
            get
            {
                return groupNames;
            }

            set
            {
                groupNames = value;
            }
        }

        public BindingList<Site> Cmb3SiteNames
        {
            get
            {
                return cmb3SiteNames;
            }

            set
            {
                cmb3SiteNames = value;
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
