// <copyright file="AddWindowViewModel.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using CommonLibrary;
    using Prism.Commands;

    /// <summary>
    /// View model for add actions
    /// </summary>
    public class AddWindowViewModel : INotifyPropertyChanged
    {
        #region fields

        /// <summary>
        /// Represent instance of client
        /// </summary>
        private LKClientService client = null;

        /// <summary>
        /// Represent instance of update info which will be sent to service
        /// </summary>
        private UpdateInfo updateInfo = null;

        /// <summary>
        /// Represent instance of site
        /// </summary>
        private Site site = null;

        /// <summary>
        /// Represent instance of group
        /// </summary>
        private Group group = null;

        /// <summary>
        /// Represent instance of generator
        /// </summary>
        private Generator generator = null;

        /// <summary>
        /// Use for showing or closing AddWindow
        /// </summary>
        private AddWindow win1 = null;

        /// <summary>
        /// name of generator
        /// </summary>
        private string name;

        /// <summary>
        /// active power of generator
        /// </summary>
        private string activePower;

        /// <summary>
        /// min of active power
        /// </summary>
        private string powerMin;

        /// <summary>
        /// max of active power
        /// </summary>
        private string powerMax;

        /// <summary>
        /// price of generator
        /// </summary>
        private string price;

        /// <summary>
        /// combo box, has generator measurement
        /// </summary>
        private bool cmbHasMeasSelectedItem;

        /// <summary>
        /// generator type
        /// </summary>
        private GeneratorType cmbGeneratorTypeSelectedItem;

        /// <summary>
        /// combo box, working mode
        /// </summary>
        private WorkingMode cmbWorkingModeSelectedItem;

        /// <summary>
        /// if selected, type new site and group
        /// </summary>
        private bool radioButton;

        /// <summary>
        /// if selected, choose site and group from combo box
        /// </summary>
        private bool radioButton1;

        /// <summary>
        /// if selected, choose site from combo box, and type group name
        /// </summary>
        private bool radioButton2;

        /// <summary>
        /// if radioButton selected, type site name
        /// </summary>
        private string siteName;

        /// <summary>
        /// if radioButton selected, type group name
        /// </summary>
        private string groupName;

        /// <summary>
        /// if radioButton2 selected, type group name
        /// </summary>
        private string txbGroupName;

        /// <summary>
        /// if radioButton1 selected, choose site from combo box
        /// </summary>
        private Site cmbSiteNameSelectedItem;

        /// <summary>
        /// if radioButton1 selected, choose group from combo box
        /// </summary>
        private Group cmb2GroupNameSelectedItem;

        /// <summary>
        /// if radioButton2 selected, choose site from combo box
        /// </summary>
        private Site cmb3SiteNameSelectedItem;

        /// <summary>
        /// visible if radioButton is selected
        /// </summary>
        private Visibility txb8Visibility;

        /// <summary>
        /// visible if radioButton is selected
        /// </summary>
        private Visibility txb9Visibility;

        /// <summary>
        /// visible if radioButton1 is selected
        /// </summary>
        private Visibility cmbVisibility;

        /// <summary>
        /// visible if radioButton1 is selected
        /// </summary>
        private Visibility cmb2Visibility;

        /// <summary>
        /// visible if radioButton2 is selected
        /// </summary>
        private Visibility cmb3Visibility;

        /// <summary>
        /// visible if radioButton2 is selected
        /// </summary>
        private Visibility txbVisibility;

        /// <summary>
        /// cancel command
        /// </summary>
        private DelegateCommand cancelCommand;

        /// <summary>
        /// create command
        /// </summary>
        private DelegateCommand createCommand;

        /// <summary>
        /// click on add button
        /// </summary>
        private DelegateCommand clickAddCommand;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddWindowViewModel" /> class.
        /// </summary>
        /// <param name="client">instance of LKClientService</param>
        public AddWindowViewModel(LKClientService client)
        {
            this.client = client;
            this.Txb8Visibility = Visibility.Visible;
            this.Txb9Visibility = Visibility.Visible;
            this.CmbVisibility = Visibility.Hidden;
            this.Cmb2Visibility = Visibility.Hidden;
            this.Cmb3Visibility = Visibility.Hidden;
            this.TxbVisibility = Visibility.Hidden;
            this.radioButton = true;
        }

        #endregion

        #region AddWindowProperties

        /// <summary>
        /// The source of the event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets instance of add window
        /// </summary>
        public AddWindow AddWin
        {
            get
            {
                return this.win1;
            }

            set
            {
                this.win1 = value;
            }
        }

        /// <summary>
        /// Gets or sets instance of client
        /// </summary>
        public LKClientService Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }

        /// <summary>
        /// Gets or sets generator name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets active power of generator
        /// </summary>
        public string ActivePower
        {
            get
            {
                return this.activePower;
            }

            set
            {
                this.activePower = value;
                this.RaisePropertyChanged("ActivePower");
            }
        }

        /// <summary>
        /// Gets or sets min value of active power
        /// </summary>
        public string PMin
        {
            get
            {
                return this.powerMin;
            }

            set
            {
                this.powerMin = value;
                this.RaisePropertyChanged("PMin");
            }
        }

        /// <summary>
        /// Gets or sets max value of active power
        /// </summary>
        public string PMax
        {
            get
            {
                return this.powerMax;
            }

            set
            {
                this.powerMax = value;
                this.RaisePropertyChanged("PMax");
            }
        }

        /// <summary>
        /// Gets or sets price of generator
        /// </summary>
        public string Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
                this.RaisePropertyChanged("Price");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether generator has measurements or not
        /// </summary>
        public bool CmbHasMeasSelectedItem
        {
            get
            {
                return this.cmbHasMeasSelectedItem;
            }

            set
            {
                this.cmbHasMeasSelectedItem = value;
                this.RaisePropertyChanged("CmbHasMeasSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets type of generator
        /// </summary>
        public GeneratorType CmbGeneratorTypeSelectedItem
        {
            get
            {
                return this.cmbGeneratorTypeSelectedItem;
            }

            set
            {
                this.cmbGeneratorTypeSelectedItem = value;
                this.RaisePropertyChanged("CmbGeneratorTypeSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets working mode
        /// </summary>
        public WorkingMode CmbWorkingModeSelectedItem
        {
            get
            {
                return this.cmbWorkingModeSelectedItem;
            }

            set
            {
                this.cmbWorkingModeSelectedItem = value;
                this.RaisePropertyChanged("CmbWorkingModeSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether visibility of others input fields
        /// </summary>
        public bool RadioButton
        {
            get
            {
                return this.radioButton;
            }

            set
            {
                this.radioButton = value;
                this.RaisePropertyChanged("RadioButton");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether visibility of others input fields
        /// </summary>
        public bool RadioButton1
        {
            get
            {
                return this.radioButton1;
            }

            set
            {
                this.radioButton1 = value;
                this.RaisePropertyChanged("RadioButton1");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether visibility of others input fields
        /// </summary>
        public bool RadioButton2
        {
            get
            {
                return this.radioButton2;
            }

            set
            {
                this.radioButton2 = value;
                this.RaisePropertyChanged("RadioButton2");
            }
        }

        /// <summary>
        /// Gets or sets site name
        /// </summary>
        public string SiteName
        {
            get
            {
                return this.siteName;
            }

            set
            {
                this.siteName = value;
                this.RaisePropertyChanged("SiteName");
            }
        }

        /// <summary>
        /// Gets or sets group name
        /// </summary>
        public string GroupName
        {
            get
            {
                return this.groupName;
            }

            set
            {
                this.groupName = value;
                this.RaisePropertyChanged("GroupName");
            }
        }

        /// <summary>
        /// Gets or sets text box, which indicate group name
        /// </summary>
        public string TxbGroupName
        {
            get
            {
                return this.txbGroupName;
            }

            set
            {
                this.txbGroupName = value;
                this.RaisePropertyChanged("TxbGroupName");
            }
        }

        /// <summary>
        /// Gets or sets combo box with sites 
        /// </summary>
        public Site CmbSiteNameSelectedItem
        {
            get
            {
                return this.cmbSiteNameSelectedItem;
            }

            set
            {
                this.cmbSiteNameSelectedItem = value;
                this.RaisePropertyChanged("CmbSiteNameSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets combo box with groups
        /// </summary>
        public Group Cmb2GroupNameSelectedItem
        {
            get
            {
                return this.cmb2GroupNameSelectedItem;
            }

            set
            {
                this.cmb2GroupNameSelectedItem = value;
                this.RaisePropertyChanged("Cmb2GroupNameSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets combo box with sites
        /// </summary>
        public Site Cmb3SiteNameSelectedItem
        {
            get
            {
                return this.cmb3SiteNameSelectedItem;
            }

            set
            {
                this.cmb3SiteNameSelectedItem = value;
                this.RaisePropertyChanged("Cmb3SiteNameSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets textBox8 visibility
        /// </summary>
        public Visibility Txb8Visibility
        {
            get
            {
                return this.txb8Visibility;
            }

            set
            {
                this.txb8Visibility = value;
                this.RaisePropertyChanged("Txb8Visibility");
            }
        }

        /// <summary>
        /// Gets or sets textBox9 visibility
        /// </summary>
        public Visibility Txb9Visibility
        {
            get
            {
                return this.txb9Visibility;
            }

            set
            {
                this.txb9Visibility = value;
                this.RaisePropertyChanged("Txb9Visibility");
            }
        }

        /// <summary>
        /// Gets or sets combo box visibility
        /// </summary>
        public Visibility CmbVisibility
        {
            get
            {
                return this.cmbVisibility;
            }

            set
            {
                this.cmbVisibility = value;
                this.RaisePropertyChanged("CmbVisibility");
            }
        }

        /// <summary>
        /// Gets or sets combo box visibility
        /// </summary>
        public Visibility Cmb2Visibility
        {
            get
            {
                return this.cmb2Visibility;
            }

            set
            {
                this.cmb2Visibility = value;
                this.RaisePropertyChanged("Cmb2Visibility");
            }
        }

        /// <summary>
        /// Gets or sets combo box visibility
        /// </summary>
        public Visibility Cmb3Visibility
        {
            get
            {
                return this.cmb3Visibility;
            }

            set
            {
                this.cmb3Visibility = value;
                this.RaisePropertyChanged("Cmb3Visibility");
            }
        }

        /// <summary>
        /// Gets or sets text box visibility
        /// </summary>
        public Visibility TxbVisibility
        {
            get
            {
                return this.txbVisibility;
            }

            set
            {
                this.txbVisibility = value;
                this.RaisePropertyChanged("TxbVisibility");
            }
        }

        #endregion

        #region CreateCommandProperty

        /// <summary>
        /// Gets information if create command can be executed
        /// </summary>
        public DelegateCommand CreateCommand
        {
            get
            {
                if (this.createCommand == null)
                {
                    this.createCommand = new DelegateCommand(this.CreateCommandAction, this.ValidationForCreate);
                }

                return this.createCommand;
            }
        }

        #endregion

        #region CancelCommandProperty

        /// <summary>
        /// Gets and execute cancel command
        /// </summary>
        public DelegateCommand CancelCommand
        {
            get
            {
                if (this.cancelCommand == null)
                {
                    this.cancelCommand = new DelegateCommand(this.CancelCommandAction);
                }

                return this.cancelCommand;
            }
        }

        #endregion

        #region ClickAddCommand

        /// <summary>
        /// Gets and execute add command
        /// </summary>
        public DelegateCommand ClickAddCommand
        {
            get
            {
                if (this.clickAddCommand == null)
                {
                    this.clickAddCommand = new DelegateCommand(this.AddClickCommandAction);
                }

                return this.clickAddCommand;
            }
        }

        /// <summary>
        /// Show window for adding generator
        /// </summary>
        public void AddClickCommandAction()
        {
            this.win1 = new AddWindow(this.Client.DataContext);
            this.win1.ShowDialog();
        }

        #endregion

        #region ClearInputFields

        /// <summary>
        /// This function is called when user click on create or cancel in AddWindow and used for refresh fields
        /// </summary>
        private void ClearInputFields()
        {
            this.Name = string.Empty;
            this.ActivePower = string.Empty;
            this.PMin = string.Empty;
            this.PMax = string.Empty;
            this.Price = string.Empty;
            this.SiteName = string.Empty;
            this.GroupName = string.Empty;
            this.TxbGroupName = string.Empty;
            this.Cmb2GroupNameSelectedItem = null;
            this.Cmb3SiteNameSelectedItem = null;
            this.CmbSiteNameSelectedItem = null;
            this.Txb8Visibility = Visibility.Visible;
            this.Txb9Visibility = Visibility.Visible;
            this.CmbVisibility = Visibility.Hidden;
            this.Cmb2Visibility = Visibility.Hidden;
            this.Cmb3Visibility = Visibility.Hidden;
            this.TxbVisibility = Visibility.Hidden;
            this.RadioButton = true;
            this.RadioButton1 = false;
            this.RadioButton2 = false;
            this.group = null;
            this.generator = null;
            this.site = null;

            if (this.win1 != null)
            {
                this.win1.Close();
            }
        }

        #endregion

        #region CreateCommand

        /// <summary>
        /// Input fields validation
        /// </summary>
        /// <returns>true if validation passed, or false</returns>
        private bool ValidationForCreate()
        {
            bool canExecute = true;

            if (this.Client.CheckStringInputField(this.Name) && this.Client.CheckDoubleInputField(this.ActivePower) &&
                this.Client.CheckDoubleInputField(this.PMin) && this.Client.CheckDoubleInputField(this.PMax) &&
                this.Client.CheckDoubleInputField(this.Price))
            {
                if (double.Parse(this.PMin) > double.Parse(this.PMax))
                {
                    canExecute = false;
                }
                else
                {
                    if (double.Parse(this.ActivePower) > double.Parse(this.PMax) || double.Parse(this.ActivePower) < double.Parse(this.PMin))
                    {
                        canExecute = false;
                    }
                }
            }
            else
            {
                canExecute = false;
            }

            if (this.RadioButton)
            {
                if (!string.IsNullOrEmpty(this.SiteName) && !string.IsNullOrEmpty(this.groupName))
                {
                    this.site = new Site()
                    {
                        Name = this.SiteName
                    };

                    this.group = new Group()
                    {
                        Name = this.GroupName
                    };
                }
                else
                {
                    if (!this.Client.CheckStringInputField(this.SiteName) || !this.Client.CheckStringInputField(this.GroupName))
                    {
                        canExecute = false;
                    }
                }
            }
            else if (this.RadioButton1)
            {
                if (this.CmbSiteNameSelectedItem != null && this.Cmb2GroupNameSelectedItem != null)
                {
                    this.site = null;
                    this.group = null;
                }
                else
                {
                    if (this.CmbSiteNameSelectedItem != null || this.Cmb2GroupNameSelectedItem != null)
                    {
                        canExecute = false;
                    }
                }
            }
            else if (this.RadioButton2)
            {
                if (this.Cmb3SiteNameSelectedItem != null && !string.IsNullOrEmpty(this.TxbGroupName))
                {
                    this.site = null;
                    this.group = new Group()
                    {
                        Name = this.TxbGroupName,
                        SiteID = this.Cmb3SiteNameSelectedItem.MRID
                    };
                }
                else
                {
                    if (this.Cmb3SiteNameSelectedItem != null || this.Client.CheckStringInputField(this.TxbGroupName))
                    {
                        canExecute = false;
                    }
                }
            }

            if (canExecute)
            {
                this.CreateInstanceOfGenerator();

                if (this.RadioButton1)
                {
                    if (this.Cmb2GroupNameSelectedItem != null)
                    {
                        this.generator.GroupID = this.Cmb2GroupNameSelectedItem.MRID;
                    }
                }
            }

            return canExecute;
        }

        /// <summary>
        /// When validation passed, this function will create instance of generator
        /// </summary>
        private void CreateInstanceOfGenerator()
        {
            this.generator = new Generator()
            {
                ActivePower = double.Parse(this.ActivePower),
                GeneratorType = this.CmbGeneratorTypeSelectedItem,
                HasMeasurment = this.CmbHasMeasSelectedItem,
                Name = this.Name,
                Pmax = double.Parse(this.PMax),
                Pmin = double.Parse(this.PMin),
                Price = double.Parse(this.Price),
                WorkingMode = this.CmbWorkingModeSelectedItem
            };
        }

        /// <summary>
        /// Create command which will be sent to a service
        /// </summary>
        private void CreateCommandAction()
        {
            List<Generator> generators = new List<Generator>(1)
                {
                    this.generator
                };

            List<Site> sites = null;
            if (this.site != null)
            {
                sites = new List<Site>(1)
                    {
                        this.site
                    };
            }

            List<Group> groups = null;
            if (this.group != null)
            {
                groups = new List<Group>(1)
                    {
                        this.group
                    };
            }

            this.updateInfo = new UpdateInfo()
            {
                Generators = generators,
                Groups = groups,
                Sites = sites,
                UpdateType = UpdateType.ADD
            };

            try
            {
                this.Client.Command(this.updateInfo);
                this.ClearInputFields();
            }
            catch
            {
                MessageBox.Show("Error during execution Create command.");
            }
        }

        #endregion

        #region CancelCommand

        /// <summary>
        /// Clear all input fields when user click on cancel button
        /// </summary>
        private void CancelCommandAction()
        {
            this.ClearInputFields();
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

                if (propName.Equals("Name") || propName.Equals("ActivePower") || propName.Equals("PMin") || propName.Equals("PMax") ||
                    propName.Equals("Price") || propName.Equals("CmbHasMeasSelectedItem") ||
                    propName.Equals("CmbGeneratorTypeSelectedItem") || propName.Equals("CmbWorkingModeSelectedItem") ||
                    propName.Equals("SiteName") || propName.Equals("GroupName") || propName.Equals("TxbGroupName") ||
                    propName.Equals("Cmb2GroupNameSelectedItem") || propName.Equals("Cmb3SiteNameSelectedItem"))
                {
                    this.CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("CmbSiteNameSelectedItem"))
                {
                    Site s = this.CmbSiteNameSelectedItem;

                    if (s != null)
                    {
                        this.Client.GroupNames.Clear();
                        foreach (Group g in this.Client.Groups)
                        {
                            if (s.MRID.Equals(g.SiteID))
                            {
                                this.Client.GroupNames.Add(g);
                            }
                        }

                        this.CreateCommand.RaiseCanExecuteChanged();
                    }
                }
                else if (propName.Equals("RadioButton"))
                {
                    if (this.radioButton)
                    {
                        this.Txb8Visibility = Visibility.Visible;
                        this.Txb9Visibility = Visibility.Visible;
                        this.CmbVisibility = Visibility.Hidden;
                        this.Cmb2Visibility = Visibility.Hidden;
                        this.Cmb3Visibility = Visibility.Hidden;
                        this.TxbVisibility = Visibility.Hidden;
                        this.CreateCommand.RaiseCanExecuteChanged();
                    }
                }
                else if (propName.Equals("RadioButton1"))
                {
                    if (this.radioButton1)
                    {
                        this.Txb8Visibility = Visibility.Hidden;
                        this.Txb9Visibility = Visibility.Hidden;
                        this.CmbVisibility = Visibility.Visible;
                        this.Cmb2Visibility = Visibility.Visible;
                        this.Cmb3Visibility = Visibility.Hidden;
                        this.TxbVisibility = Visibility.Hidden;
                        this.CreateCommand.RaiseCanExecuteChanged();
                    }
                }
                else if (propName.Equals("RadioButton2"))
                {
                    if (this.radioButton2)
                    {
                        this.Txb8Visibility = Visibility.Hidden;
                        this.Txb9Visibility = Visibility.Hidden;
                        this.CmbVisibility = Visibility.Hidden;
                        this.Cmb2Visibility = Visibility.Hidden;
                        this.Cmb3Visibility = Visibility.Visible;
                        this.TxbVisibility = Visibility.Visible;
                        this.CreateCommand.RaiseCanExecuteChanged();
                    }
                }
            }
        }

        #endregion
    }
}
