// <copyright file="EditRemoveViewModel.cs" company="company">
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
    using System.Windows.Controls.DataVisualization.Charting;
    using System;    /// <summary>
                     /// View model for edit and remove actions
                     /// </summary>
    public class EditRemoveViewModel : INotifyPropertyChanged
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
        /// Use for showing or closing EditWindow
        /// </summary>
        private EditWindow win1 = null;

        /// <summary>
        /// Use for showing or closing EditWindow
        /// </summary>
        private ShowDataWindow showWin = null;

        /// <summary>
        /// generator selected in the table
        /// </summary>
        private Generator selectedItem;

        /// <summary>
        /// name of generator
        /// </summary>
        private string editName;

        /// <summary>
        /// active power of generator
        /// </summary>
        private string editActivePower;

        /// <summary>
        /// min of active power
        /// </summary>
        private string editPMin;

        /// <summary>
        /// max of active power
        /// </summary>
        private string editPMax;

        /// <summary>
        /// price of generator
        /// </summary>
        private string editPrice;

        /// <summary>
        /// combo box, has generator measurement
        /// </summary>
        private bool editCmbHasMeasSelectedItem;

        /// <summary>
        /// generator type
        /// </summary>
        private GeneratorType editCmbGeneratorTypeSelectedItem;

        /// <summary>
        /// combo box, working mode
        /// </summary>
        private WorkingMode editCmbWorkingModeSelectedItem;

        /// <summary>
        /// if selected, type new site and group
        /// </summary>
        private bool editRadioButton;

        /// <summary>
        /// if selected, choose site and group from combo box
        /// </summary>
        private bool editRadioButton1;

        /// <summary>
        /// if selected, choose site from combo box, and type group name
        /// </summary>
        private bool editRadioButton2;

        /// <summary>
        /// if radioButton1 selected, type site name
        /// </summary>
        private string editSiteName;

        /// <summary>
        /// if radioButton1 selected, type group name
        /// </summary>
        private string editGroupName;

        /// <summary>
        /// if radioButton2 selected, type group name
        /// </summary>
        private string editTxbGroupName;

        /// <summary>
        /// if radioButton1 selected, choose site from combo box
        /// </summary>
        private Site editCmbSiteNameSelectedItem;

        /// <summary>
        /// if radioButton1 selected, choose group from combo box
        /// </summary>
        private Group editCmb2GroupNameSelectedItem;

        /// <summary>
        /// if radioButton2 selected, choose site from combo box
        /// </summary>
        private Site editCmb3SiteNameSelectedItem;

        /// <summary>
        /// visible if radioButton is selected
        /// </summary>
        private Visibility editTxb8Visibility;

        /// <summary>
        /// visible if radioButton is selected
        /// </summary>
        private Visibility editTxb9Visibility;

        /// <summary>
        /// visible if radioButton1 is selected
        /// </summary>
        private Visibility editCmbVisibility;

        /// <summary>
        /// visible if radioButton1 is selected
        /// </summary>
        private Visibility editCmb2Visibility;

        /// <summary>
        /// visible if radioButton2 is selected
        /// </summary>
        private Visibility editCmb3Visibility;

        /// <summary>
        /// visible if radioButton2 is selected
        /// </summary>
        private Visibility editTxbVisibility;

        /// <summary>
        /// edit command
        /// </summary>
        private DelegateCommand editCommand;

        /// <summary>
        /// click on edit button
        /// </summary>
        private DelegateCommand clickEditCommand;

        /// <summary>
        /// cancel command
        /// </summary>
        private DelegateCommand editCancelCommand;

        /// <summary>
        /// remove command
        /// </summary>
        private DelegateCommand removeCommand;

        /// <summary>
        /// show command
        /// </summary>
        private DelegateCommand showDataCommand;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditRemoveViewModel" /> class.
        /// </summary>
        /// <param name="client">instance of LKClientService</param>
        public EditRemoveViewModel(LKClientService client)
        {
            this.client = client;
            this.EditTxb8Visibility = Visibility.Hidden;
            this.EditTxb9Visibility = Visibility.Hidden;
            this.EditCmbVisibility = Visibility.Visible;
            this.EditCmb2Visibility = Visibility.Visible;
            this.EditCmb3Visibility = Visibility.Hidden;
            this.EditTxbVisibility = Visibility.Hidden;
            this.editRadioButton1 = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The source of the event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
        /// Gets or sets instance of edit window
        /// </summary>
        public EditWindow EditWin
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
        /// Gets or sets selected generator
        /// </summary>
        public Generator SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                this.selectedItem = value;
                this.RaisePropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets generator name
        /// </summary>
        public string EditName
        {
            get
            {
                return this.editName;
            }

            set
            {
                this.editName = value;
                this.RaisePropertyChanged("EditName");
            }
        }

        /// <summary>
        /// Gets or sets active power of generator
        /// </summary>
        public string EditActivePower
        {
            get
            {
                return this.editActivePower;
            }

            set
            {
                this.editActivePower = value;
                this.RaisePropertyChanged("EditActivePower");
            }
        }

        /// <summary>
        /// Gets or sets min value of active power
        /// </summary>
        public string EditPMin
        {
            get
            {
                return this.editPMin;
            }

            set
            {
                this.editPMin = value;
                this.RaisePropertyChanged("EditPMin");
            }
        }

        /// <summary>
        /// Gets or sets max value of active power
        /// </summary>
        public string EditPMax
        {
            get
            {
                return this.editPMax;
            }

            set
            {
                this.editPMax = value;
                this.RaisePropertyChanged("EditPMax");
            }
        }

        /// <summary>
        /// Gets or sets price of generator
        /// </summary>
        public string EditPrice
        {
            get
            {
                return this.editPrice;
            }

            set
            {
                this.editPrice = value;
                this.RaisePropertyChanged("EditPrice");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether generator has measurements or not
        /// </summary>
        public bool EditCmbHasMeasSelectedItem
        {
            get
            {
                return this.editCmbHasMeasSelectedItem;
            }

            set
            {
                this.editCmbHasMeasSelectedItem = value;
                this.RaisePropertyChanged("EditCmbHasMeasSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets type of generator
        /// </summary>
        public GeneratorType EditCmbGeneratorTypeSelectedItem
        {
            get
            {
                return this.editCmbGeneratorTypeSelectedItem;
            }

            set
            {
                this.editCmbGeneratorTypeSelectedItem = value;
                this.RaisePropertyChanged("EditCmbGeneratorTypeSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets working mode
        /// </summary>
        public WorkingMode EditCmbWorkingModeSelectedItem
        {
            get
            {
                return this.editCmbWorkingModeSelectedItem;
            }

            set
            {
                this.editCmbWorkingModeSelectedItem = value;
                this.RaisePropertyChanged("EditCmbWorkingModeSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether visibility of others input fields
        /// </summary>
        public bool EditRadioButton
        {
            get
            {
                return this.editRadioButton;
            }

            set
            {
                this.editRadioButton = value;
                this.RaisePropertyChanged("EditRadioButton");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether visibility of others input fields
        /// </summary>
        public bool EditRadioButton1
        {
            get
            {
                return this.editRadioButton1;
            }

            set
            {
                this.editRadioButton1 = value;
                this.RaisePropertyChanged("EditRadioButton1");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether visibility of others input fields
        /// </summary>
        public bool EditRadioButton2
        {
            get
            {
                return this.editRadioButton2;
            }

            set
            {
                this.editRadioButton2 = value;
                this.RaisePropertyChanged("EditRadioButton2");
            }
        }

        /// <summary>
        /// Gets or sets site name
        /// </summary>
        public string EditSiteName
        {
            get
            {
                return this.editSiteName;
            }

            set
            {
                this.editSiteName = value;
                this.RaisePropertyChanged("EditSiteName");
            }
        }

        /// <summary>
        /// Gets or sets group name
        /// </summary>
        public string EditGroupName
        {
            get
            {
                return this.editGroupName;
            }

            set
            {
                this.editGroupName = value;
                this.RaisePropertyChanged("EditGroupName");
            }
        }

        /// <summary>
        /// Gets or sets text box, which indicate group name
        /// </summary>
        public string EditTxbGroupName
        {
            get
            {
                return this.editTxbGroupName;
            }

            set
            {
                this.editTxbGroupName = value;
                this.RaisePropertyChanged("EditTxbGroupName");
            }
        }

        /// <summary>
        /// Gets or sets combo box with sites 
        /// </summary>
        public Site EditCmbSiteNameSelectedItem
        {
            get
            {
                return this.editCmbSiteNameSelectedItem;
            }

            set
            {
                this.editCmbSiteNameSelectedItem = value;
                this.RaisePropertyChanged("EditCmbSiteNameSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets combo box with groups
        /// </summary>
        public Group EditCmb2GroupNameSelectedItem
        {
            get
            {
                return this.editCmb2GroupNameSelectedItem;
            }

            set
            {
                this.editCmb2GroupNameSelectedItem = value;
                this.RaisePropertyChanged("EditCmb2GroupNameSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets combo box with sites
        /// </summary>
        public Site EditCmb3SiteNameSelectedItem
        {
            get
            {
                return this.editCmb3SiteNameSelectedItem;
            }

            set
            {
                this.editCmb3SiteNameSelectedItem = value;
                this.RaisePropertyChanged("EditCmb3SiteNameSelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets textBox8 visibility
        /// </summary>
        public Visibility EditTxb8Visibility
        {
            get
            {
                return this.editTxb8Visibility;
            }

            set
            {
                this.editTxb8Visibility = value;
                this.RaisePropertyChanged("EditTxb8Visibility");
            }
        }

        /// <summary>
        /// Gets or sets textBox9 visibility
        /// </summary>
        public Visibility EditTxb9Visibility
        {
            get
            {
                return this.editTxb9Visibility;
            }

            set
            {
                this.editTxb9Visibility = value;
                this.RaisePropertyChanged("EditTxb9Visibility");
            }
        }

        /// <summary>
        /// Gets or sets combo box visibility
        /// </summary>
        public Visibility EditCmbVisibility
        {
            get
            {
                return this.editCmbVisibility;
            }

            set
            {
                this.editCmbVisibility = value;
                this.RaisePropertyChanged("EditCmbVisibility");
            }
        }

        /// <summary>
        /// Gets or sets combo box visibility
        /// </summary>
        public Visibility EditCmb2Visibility
        {
            get
            {
                return this.editCmb2Visibility;
            }

            set
            {
                this.editCmb2Visibility = value;
                this.RaisePropertyChanged("EditCmb2Visibility");
            }
        }

        /// <summary>
        /// Gets or sets combo box visibility
        /// </summary>
        public Visibility EditCmb3Visibility
        {
            get
            {
                return this.editCmb3Visibility;
            }

            set
            {
                this.editCmb3Visibility = value;
                this.RaisePropertyChanged("EditCmb3Visibility");
            }
        }

        /// <summary>
        /// Gets or sets text box visibility
        /// </summary>
        public Visibility EditTxbVisibility
        {
            get
            {
                return this.editTxbVisibility;
            }

            set
            {
                this.editTxbVisibility = value;
                this.RaisePropertyChanged("EditTxbVisibility");
            }
        }

        #endregion

        #region EditCancelCommandProperty

        /// <summary>
        /// Gets and execute cancel command
        /// </summary>
        public DelegateCommand EditCancelCommand
        {
            get
            {
                if (this.editCancelCommand == null)
                {
                    this.editCancelCommand = new DelegateCommand(this.ClearInputFieldsEditWindow);
                }

                return this.editCancelCommand;
            }
        }

        #endregion

        #region RemoveCommandProperty

        /// <summary>
        /// Gets information if remove command can be executed
        /// </summary>
        public DelegateCommand RemoveCommand
        {
            get
            {
                if (this.removeCommand == null)
                {
                    this.removeCommand = new DelegateCommand(this.RemoveCommandAction, this.CanExecute);
                }

                return this.removeCommand;
            }
        }

        #endregion

        #region ClickEditCommandProperty

        /// <summary>
        /// Gets information if edit button is enabled
        /// </summary>
        public DelegateCommand ClickEditCommand
        {
            get
            {
                if (this.clickEditCommand == null)
                {
                    this.clickEditCommand = new DelegateCommand(this.EditClickCommandAction, this.CanExecute);
                }

                return this.clickEditCommand;
            }
        }

        #endregion

        #region ShowCommandProperty

        /// <summary>
        /// Gets information if show command can be executed
        /// </summary>
        public DelegateCommand ShowDataCommand
        {
            get
            {
                if (this.showDataCommand == null)
                {
                    this.showDataCommand = new DelegateCommand(this.ShowCommandAction, this.CanShowDataExecute);
                }

                return this.showDataCommand;
            }
        }

        #endregion

        private bool CanShowDataExecute()
        {
            if (this.SelectedItem == null)
            {
                return false;
            }

            Generator gen = (Generator)this.SelectedItem;

            return gen.HasMeasurment;
        }

        private void ShowCommandAction()
        {
            this.showWin = new ShowDataWindow(this.Client.DataContext);
            this.showWin.ShowDialog();

            // ((LineSeries)this.showWin.mcChart.Series[0]).ItemsSource =
            //    new KeyValuePair<DateTime, int>[]{
            //    new KeyValuePair<DateTime,int>(DateTime.Now, 100),
            //    new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(1), 130),
            //    new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(2), 150),
            //    new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(3), 125),
            //    new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(4),155) };
        }

        #region EditCommand

        /// <summary>
        /// Gets information if edit command can be executed
        /// </summary>
        public DelegateCommand EditCommand
        {
            get
            {
                if (this.editCommand == null)
                {
                    this.editCommand = new DelegateCommand(this.EditCommandAction, this.CanExecuteEditCommand);
                }

                return this.editCommand;
            }
        }

        /// <summary>
        /// Validation for edit input fields
        /// </summary>
        /// <returns>true if validation passed, otherwise false</returns>
        private bool CanExecuteEditCommand()
        {
            if (this.SelectedItem == null)
            {
                return false;
            }

            return this.ValidationForEdit();
        }

        /// <summary>
        /// Input fields validation
        /// </summary>
        /// <returns>true if validation passed, otherwise false</returns>
        private bool ValidationForEdit()
        {
            bool canExecute = true;

            if (this.Client.CheckStringInputField(this.EditName) && this.Client.CheckDoubleInputField(this.EditActivePower) &&
                this.Client.CheckDoubleInputField(this.EditPMin) && this.Client.CheckDoubleInputField(this.EditPMax) &&
                this.Client.CheckDoubleInputField(this.EditPrice))
            {
                if (double.Parse(this.EditPMin) > double.Parse(this.EditPMax))
                {
                    canExecute = false;
                }
                else
                {
                    if (double.Parse(this.EditActivePower) > double.Parse(this.EditPMax) ||
                        double.Parse(this.EditActivePower) < double.Parse(this.EditPMin))
                    {
                        canExecute = false;
                    }
                }
            }
            else
            {
                canExecute = false;
            }

            if (this.EditRadioButton)
            {
                if (!string.IsNullOrEmpty(this.EditSiteName) && !string.IsNullOrEmpty(this.EditGroupName))
                {
                    this.site = new Site()
                    {
                        Name = this.EditSiteName
                    };

                    this.group = new Group()
                    {
                        Name = this.EditGroupName
                    };
                }
                else
                {
                    if (!this.Client.CheckStringInputField(this.EditSiteName) || !this.Client.CheckStringInputField(this.EditGroupName))
                    {
                        canExecute = false;
                    }
                }
            }
            else if (this.EditRadioButton1)
            {
                if (this.EditCmbSiteNameSelectedItem != null && this.EditCmb2GroupNameSelectedItem != null)
                {
                    this.site = null;
                    this.group = null;
                }
                else
                {
                    if (this.EditCmbSiteNameSelectedItem != null || this.EditCmb2GroupNameSelectedItem != null)
                    {
                        canExecute = false;
                    }
                }
            }
            else if (this.EditRadioButton2)
            {
                if (this.EditCmb3SiteNameSelectedItem != null && !string.IsNullOrEmpty(this.EditTxbGroupName))
                {
                    this.site = null;
                    this.group = new Group()
                    {
                        Name = this.EditTxbGroupName,
                        SiteID = this.EditCmb3SiteNameSelectedItem.MRID
                    };
                }
                else
                {
                    if (this.EditCmb3SiteNameSelectedItem != null || this.Client.CheckStringInputField(this.EditTxbGroupName))
                    {
                        canExecute = false;
                    }
                }
            }

            if (canExecute)
            {
                this.CreateInstanceOfGeneratorEdit();

                if (this.EditRadioButton1)
                {
                    if (this.EditCmb2GroupNameSelectedItem != null)
                    {
                        this.generator.GroupID = this.EditCmb2GroupNameSelectedItem.MRID;
                    }
                }
            }

            return canExecute;
        }

        /// <summary>
        /// When validation passed, this function will create instance of generator
        /// </summary>
        private void CreateInstanceOfGeneratorEdit()
        {
            this.generator = new Generator()
            {
                ActivePower = double.Parse(this.EditActivePower),
                GeneratorType = this.EditCmbGeneratorTypeSelectedItem,
                HasMeasurment = this.EditCmbHasMeasSelectedItem,
                Name = this.EditName,
                Pmax = double.Parse(this.EditPMax),
                Pmin = double.Parse(this.EditPMin),
                Price = double.Parse(this.EditPrice),
                WorkingMode = this.EditCmbWorkingModeSelectedItem,
                MRID = this.SelectedItem.MRID
            };
        }

        /// <summary>
        /// Edit command which will be sent to a service
        /// </summary>
        private void EditCommandAction()
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
                UpdateType = UpdateType.UPDATE
            };

            try
            {
                this.Client.Command(this.updateInfo);
                this.ClearInputFieldsEditWindow();
            }
            catch
            {
                MessageBox.Show("Error during execution Edit command.");
            }
        }

        #endregion

        #region ClearInputFields

        /// <summary>
        /// This function is called when user click on create or cancel in EditWindow and used for refresh fields
        /// </summary>
        private void ClearInputFieldsEditWindow()
        {
            this.EditName = string.Empty;
            this.EditActivePower = string.Empty;
            this.EditPMin = string.Empty;
            this.EditPMax = string.Empty;
            this.EditPrice = string.Empty;
            this.EditSiteName = string.Empty;
            this.EditGroupName = string.Empty;
            this.EditTxbGroupName = string.Empty;
            this.EditCmb2GroupNameSelectedItem = null;
            this.EditCmb3SiteNameSelectedItem = null;
            this.EditCmbSiteNameSelectedItem = null;
            this.EditTxb8Visibility = Visibility.Hidden;
            this.EditTxb9Visibility = Visibility.Hidden;
            this.EditCmbVisibility = Visibility.Visible;
            this.EditCmb2Visibility = Visibility.Visible;
            this.EditCmb3Visibility = Visibility.Hidden;
            this.EditTxbVisibility = Visibility.Hidden;
            this.EditRadioButton = false;
            this.EditRadioButton1 = true;
            this.EditRadioButton2 = false;
            this.group = null;
            this.generator = null;
            this.site = null;
            this.SelectedItem = null;
            this.win1.Close();
        }

        #endregion

        #region RemoveCommand
        /// <summary>
        /// Check if generator is selected. If is selected, remove and edit button will be enabled, otherwise disabled.
        /// </summary>
        /// <returns>true if generator in table is selected, otherwise false</returns>
        private bool CanExecute()
        {
            return this.SelectedItem != null;
        }

        /// <summary>
        /// Remove command which will be sent to a service
        /// </summary>
        private void RemoveCommandAction()
        {
            List<Generator> generators = new List<Generator>(1)
            {
                this.generator
            };

            bool deleteGroup = true;
            foreach (Generator clientGenerator in this.Client.Generators)
            {
                if (!clientGenerator.MRID.Equals(this.generator.MRID))
                {
                    if (clientGenerator.GroupID.Equals(this.group.MRID))
                    {
                        deleteGroup = false;
                        break;
                    }
                }
            }

            bool deleteSite = true;
            List<Group> groups = null;
            List<Site> sites = null;

            if (deleteGroup)
            {
                groups = new List<Group>(1)
                {
                    this.group
                };

                foreach (Group clientGroup in this.Client.Groups)
                {
                    if (!clientGroup.MRID.Equals(this.group.MRID))
                    {
                        if (clientGroup.SiteID.Equals(this.group.SiteID))
                        {
                            deleteSite = false;
                            break;
                        }
                    }
                }

                if (deleteSite)
                {
                    sites = new List<Site>(1)
                    {
                        this.site
                    };
                }
            }

            this.updateInfo = new UpdateInfo()
            {
                Generators = generators,
                Groups = groups,
                Sites = sites,
                UpdateType = UpdateType.REMOVE
            };

            this.SelectedItem = null;

            this.Client.Command(this.updateInfo);
        }

        #endregion

        #region ClickEditCommand

        /// <summary>
        /// Fill the fields with the selected generator
        /// </summary>
        private void EditClickCommandAction()
        {
            this.EditTxb8Visibility = Visibility.Hidden;
            this.EditTxb9Visibility = Visibility.Hidden;
            this.EditCmbVisibility = Visibility.Visible;
            this.EditCmb2Visibility = Visibility.Visible;
            this.EditCmb3Visibility = Visibility.Hidden;
            this.EditTxbVisibility = Visibility.Hidden;
            this.EditRadioButton = false;
            this.EditRadioButton1 = true;
            this.EditRadioButton2 = false;
            this.EditSiteName = string.Empty;
            this.EditGroupName = string.Empty;
            this.EditTxbGroupName = string.Empty;
            this.EditCmb3SiteNameSelectedItem = null;

            this.generator = this.SelectedItem;

            this.EditName = this.generator.Name;
            this.EditActivePower = this.generator.ActivePower.ToString();
            this.EditPMin = this.generator.Pmin.ToString();
            this.EditPMax = this.generator.Pmax.ToString();
            this.EditPrice = this.generator.Price.ToString();

            this.EditCmbHasMeasSelectedItem = this.generator.HasMeasurment;
            this.EditCmbGeneratorTypeSelectedItem = this.generator.GeneratorType;
            this.EditCmbWorkingModeSelectedItem = this.generator.WorkingMode;

            this.EditCmbSiteNameSelectedItem = this.Client.GetSiteFromId(this.Client.GetGroupFromId(this.generator.GroupID).SiteID);
            this.EditCmb2GroupNameSelectedItem = this.Client.GetGroupFromId(this.generator.GroupID);

            this.group = null;
            this.generator = null;
            this.site = null;
            this.win1 = new EditWindow(this.Client.DataContext);
            this.win1.ShowDialog();
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

                if (propName.Equals("EditName") || propName.Equals("EditActivePower") || propName.Equals("EditPMin") ||
                    propName.Equals("EditPMax") || propName.Equals("EditPrice") || propName.Equals("EditCmbHasMeasSelectedItem") ||
                    propName.Equals("EditCmbGeneratorTypeSelectedItem") || propName.Equals("EditCmbWorkingModeSelectedItem") ||
                    propName.Equals("EditSiteName") || propName.Equals("EditGroupName") || propName.Equals("EditTxbGroupName") ||
                    propName.Equals("EditCmb2GroupNameSelectedItem") || propName.Equals("EditCmb3SiteNameSelectedItem"))
                {
                    this.EditCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditCmbSiteNameSelectedItem"))
                {
                    Site s = this.EditCmbSiteNameSelectedItem;

                    if (s != null)
                    {
                        this.Client.EditGroupNames.Clear();
                        foreach (Group g in this.Client.Groups)
                        {
                            if (s.MRID.Equals(g.SiteID))
                            {
                                this.Client.EditGroupNames.Add(g);
                            }
                        }

                        this.EditCommand.RaiseCanExecuteChanged();
                    }
                }
                else if (propName.Equals("EditRadioButton"))
                {
                    if (this.editRadioButton)
                    {
                        this.EditTxb8Visibility = Visibility.Visible;
                        this.EditTxb9Visibility = Visibility.Visible;
                        this.EditCmbVisibility = Visibility.Hidden;
                        this.EditCmb2Visibility = Visibility.Hidden;
                        this.EditCmb3Visibility = Visibility.Hidden;
                        this.EditTxbVisibility = Visibility.Hidden;
                        this.EditCommand.RaiseCanExecuteChanged();
                    }
                }
                else if (propName.Equals("EditRadioButton1"))
                {
                    if (this.editRadioButton1)
                    {
                        this.EditTxb8Visibility = Visibility.Hidden;
                        this.EditTxb9Visibility = Visibility.Hidden;
                        this.EditCmbVisibility = Visibility.Visible;
                        this.EditCmb2Visibility = Visibility.Visible;
                        this.EditCmb3Visibility = Visibility.Hidden;
                        this.EditTxbVisibility = Visibility.Hidden;
                        this.EditCommand.RaiseCanExecuteChanged();
                    }
                }
                else if (propName.Equals("EditRadioButton2"))
                {
                    if (this.editRadioButton2)
                    {
                        this.EditTxb8Visibility = Visibility.Hidden;
                        this.EditTxb9Visibility = Visibility.Hidden;
                        this.EditCmbVisibility = Visibility.Hidden;
                        this.EditCmb2Visibility = Visibility.Hidden;
                        this.EditCmb3Visibility = Visibility.Visible;
                        this.EditTxbVisibility = Visibility.Visible;
                        this.EditCommand.RaiseCanExecuteChanged();
                    }
                }
                else if (propName.Equals("SelectedItem"))
                {
                    if (this.SelectedItem != null)
                    {
                        this.generator = this.SelectedItem;
                        this.group = this.Client.GetGroupFromId(this.generator.GroupID);
                        this.site = this.Client.GetSiteFromId(this.group.SiteID);
                        this.ClickEditCommand.RaiseCanExecuteChanged();
                        this.RemoveCommand.RaiseCanExecuteChanged();
                    }
                }
            }
        }

        #endregion
    }
}
