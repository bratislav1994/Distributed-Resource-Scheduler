// <copyright file="LKClientService.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using CommonLibrary;
    using CommonLibrary.Interfaces;
    using System.ServiceModel;

    /// <summary>
    /// Implement interface ILKClient
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LKClientService : ILKClient
    {
        #region fields

        /// <summary>
        /// Represent service
        /// </summary>
        private ILKForClient proxy = null;

        /// <summary>
        /// Generators bind to data grid
        /// </summary>
        private BindingList<Generator> generators = null;

        /// <summary>
        /// Sites bind to combo box
        /// </summary>
        private BindingList<Site> sites = null;

        /// <summary>
        /// Groups bind to combo box
        /// </summary>
        private BindingList<Group> groups = null;

        /// <summary>
        /// Bind to combo box with true or false values
        /// </summary>
        private BindingList<bool> hasMeasurments = null;

        /// <summary>
        /// Enum bind to combo box
        /// </summary>
        private BindingList<GeneratorType> generatorTypes = null;

        /// <summary>
        /// Enum bind to combo box
        /// </summary>
        private BindingList<WorkingMode> workingModes = null;

        /// <summary>
        /// List of groups bind to combo box depending on the selected site
        /// </summary>
        private BindingList<Group> groupNames = null;

        /// <summary>
        /// List of site bind to combo box
        /// </summary>
        private BindingList<Site> siteNames = null;

        /// <summary>
        /// List of groups bind to combo box in edit window
        /// </summary>
        private BindingList<Group> editGroupNames = null;

        /// <summary>
        /// Data context of master view model
        /// </summary>
        private object dataContext = null;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LKClientService" /> class.
        /// </summary>
        public LKClientService()
        {
            this.generators = new BindingList<Generator>();
            this.sites = new BindingList<Site>();
            this.groups = new BindingList<Group>();
            this.hasMeasurments = new BindingList<bool>() { true, false };
            this.generatorTypes = new BindingList<GeneratorType>();
            this.workingModes = new BindingList<WorkingMode>();
            this.groupNames = new BindingList<Group>();
            this.editGroupNames = new BindingList<Group>();
            this.siteNames = new BindingList<Site>();

            foreach (GeneratorType genType in Enum.GetValues(typeof(GeneratorType)))
            {
                this.generatorTypes.Add(genType);
            }

            foreach (WorkingMode workMode in Enum.GetValues(typeof(WorkingMode)))
            {
                this.workingModes.Add(workMode);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The source of the event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets proxy 
        /// </summary>
        public ILKForClient Proxy
        {
            get
            {
                return this.proxy;
            }

            set
            {
                this.proxy = value;
            }
        }

        /// <summary>
        /// Gets or sets combo box of measurements
        /// </summary>
        public BindingList<bool> HasMeasurments
        {
            get
            {
                return this.hasMeasurments;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.hasMeasurments = value;
            }
        }

        /// <summary>
        /// Gets or sets types of generator
        /// </summary>
        public BindingList<GeneratorType> GeneratorTypes
        {
            get
            {
                return this.generatorTypes;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.generatorTypes = value;
            }
        }

        /// <summary>
        /// Gets or sets working mode
        /// </summary>
        public BindingList<WorkingMode> WorkingModes
        {
            get
            {
                return this.workingModes;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.workingModes = value;
            }
        }

        /// <summary>
        /// Gets or sets generators
        /// </summary>
        public BindingList<Generator> Generators
        {
            get
            {
                return this.generators;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.generators = value;
                this.RaisePropertyChanged("Generators");
            }
        }

        /// <summary>
        /// Gets or sets sites
        /// </summary>
        public BindingList<Site> Sites
        {
            get
            {
                return this.sites;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.sites = value;
            }
        }

        /// <summary>
        /// Gets or sets groups
        /// </summary>
        public BindingList<Group> Groups
        {
            get
            {
                return this.groups;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.groups = value;
            }
        }

        /// <summary>
        /// Gets or sets site names
        /// </summary>
        public BindingList<Site> SiteNames
        {
            get
            {
                return this.siteNames;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.siteNames = value;
            }
        }

        /// <summary>
        /// Gets or sets group names
        /// </summary>
        public BindingList<Group> GroupNames
        {
            get
            {
                return this.groupNames;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.groupNames = value;
            }
        }

        /// <summary>
        /// Gets or sets group names for edit window
        /// </summary>
        public BindingList<Group> EditGroupNames
        {
            get
            {
                return this.editGroupNames;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.editGroupNames = value;
            }
        }

        /// <summary>
        /// Gets or sets data context
        /// </summary>
        public object DataContext
        {
            get
            {
                return this.dataContext;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.dataContext = value;
            }
        }

        #endregion

        #region Send UpdateInfo to service

        /// <summary>
        /// Represents the method that will send to LKRES service update info object
        /// </summary>
        /// <param name="update">update info which will be sent to service</param>
        public void Command(UpdateInfo update)
        {
            try
            {
                this.proxy.Update(update);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Receive UpdateInfo

        /// <summary>
        /// Represents the method that will receive update info object from LKRES service
        /// </summary>
        /// <param name="update">update info which will be receive from service</param>
        public void Update(UpdateInfo update)
        {
            if (update == null)
            {
                throw new InvalidDataException();
            }

            switch (update.UpdateType)
            {
                case UpdateType.ADD:
                    this.Generators.Add(update.Generators[0]);

                    if (update.Groups != null)
                    {
                        this.Groups.Add(update.Groups[0]);
                    }

                    if (update.Sites != null)
                    {
                        this.Sites.Add(update.Sites[0]);
                    }

                    break;
                case UpdateType.REMOVE:
                    if (update.Generators != null)
                    {
                        this.Generators.Remove(this.Generators.SingleOrDefault(g => g.MRID.Equals(update.Generators[0].MRID)));
                    }

                    if (update.Groups != null)
                    {
                        this.Groups.Remove(this.Groups.SingleOrDefault(g => g.MRID.Equals(update.Groups[0].MRID)));
                    }

                    if (update.Sites != null)
                    {
                        this.Sites.Remove(this.Sites.SingleOrDefault(s => s.MRID.Equals(update.Groups[0].SiteID)));
                    }

                    break;
                case UpdateType.UPDATE:
                    if (update.Generators != null)
                    {
                        foreach (Generator gen in update.Generators)
                        {
                            Generator gen1 = this.Generators.SingleOrDefault(p => p.MRID.Equals(gen.MRID));

                            if (gen1 != null)
                            {
                                gen1.ActivePower = gen.ActivePower;
                                gen1.GroupID = gen.GroupID;
                                gen1.BasePoint = gen.BasePoint;
                                gen1.GeneratorType = gen.GeneratorType;
                                gen1.HasMeasurment = gen.HasMeasurment;
                                gen1.Pmax = gen.Pmax;
                                gen1.Pmin = gen.Pmin;
                                gen1.Price = gen.Price;
                                gen1.SetPoint = gen.SetPoint;
                                gen1.WorkingMode = gen.WorkingMode;
                                gen1.Name = gen.Name;
                            }
                        }
                    }

                    if (update.Groups != null)
                    {
                        this.Groups.Add(update.Groups[0]);
                    }

                    if (update.Sites != null)
                    {
                        this.Sites.Add(update.Sites[0]);
                    }

                    break;
            }
        }

        #endregion

        #region login and register

        /// <summary>
        /// register method
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="password">password box</param>
        public bool Registration(string username, string password)
        {
            try
            {
                this.Proxy.Registration(username, password);
                this.Proxy.Login(username, password);
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        /// <summary>
        /// login method
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="password">password box</param>
        public bool LogIn(string username, string password)
        {
            try
            {
                this.Proxy.Login(username, password);
                this.Initialize();
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return true;
        }

        /// <summary>
        /// only called in start, when user login
        /// </summary>
        public void Initialize()
        {
            UpdateInfo getAllFromService = this.Proxy.GetMySystem();

            if (getAllFromService.Generators.Count != 0)
            {
                getAllFromService.Generators.ForEach(x => { Generators.Add(x); });
            }

            if (getAllFromService.Sites.Count != 0)
            {
                getAllFromService.Sites.ForEach(x => { Sites.Add(x); });
            }

            if (getAllFromService.Groups.Count != 0)
            {
                getAllFromService.Groups.ForEach(x => { Groups.Add(x); });
            }
        }

        #endregion

        #region GetMeasurement

        /// <summary>
        /// Call service to get measurement for selected generator
        /// </summary>
        /// <param name="id">id from selected generator</param>
        /// <returns>all measurement for selected generator</returns>
        public List<KeyValuePair<DateTime, double>> GetMeasurements(string id)
        {
            return this.Proxy.GetMeasurements(id);
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// Represents the method that will try to find group which contains id given from parameter and return that group if exist
        /// </summary>
        /// <param name="id">id from generator group id</param>
        /// <returns>group if exists or null if can not be found</returns>
        public Group GetGroupFromId(string id)
        {
            Group group = this.Groups.SingleOrDefault(p => p.MRID.Equals(id));
            if (group != null)
            {
                return group;
            }

            return null;
        }

        /// <summary>
        /// Represents the method that will try to find site which contains id given from parameter and return that group if exist
        /// </summary>
        /// <param name="id">id from generator group id</param>
        /// <returns>site if exists or null if can not be found</returns>
        public Site GetSiteFromId(string id)
        {
            Site site = this.Sites.SingleOrDefault(p => p.MRID.Equals(id));
            if (site != null)
            {
                return site;
            }

            return null;
        }

        /// <summary>
        /// Represents the method that will check is input field empty or not
        /// </summary>
        /// <param name="txb">input field</param>
        /// <returns>true if validation is ok, or false</returns>
        public bool CheckStringInputField(string txb)
        {
            return !string.IsNullOrEmpty(txb);
        }

        /// <summary>
        /// Represents the method that will check if input field can be parsed to double
        /// </summary>
        /// <param name="txb">input field</param>
        /// <returns>true if validation is ok, or false</returns>
        public bool CheckDoubleInputField(string txb)
        {
            if (string.IsNullOrEmpty(txb))
            {
                return false;
            }

            try
            {
                if (double.Parse(txb) < 1)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion

        #region RaisePropertyChanged

        /// <summary>
        /// Represents the method that will handle the System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        /// event raised when a property is changed on a component.
        /// </summary>
        /// <param name="propName">changed property</param>
        private void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}
