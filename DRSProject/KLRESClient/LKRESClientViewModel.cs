using CommonLibrary;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KLRESClient
{
    public class LKRESClientViewModel : INotifyPropertyChanged
    {
        private UpdateInfo updateInfo = null;
        private LKClientService client;
        private Site site = null;
        private Group group = null;
        private Generator generator = null;

        public LKRESClientViewModel()
        {
            client = new LKClientService();
            //radioButton = true;
        }

        public LKClientService Client
        {
            get
            {
                if (client == null)
                {
                    client = new LKClientService();
                }
                return client;
            }
            set
            {
                client = value;
            }
        }

        #region TableGenerator
        private object selectedItem;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private string genMRID;
        public string GenMRID
        {
            get
            {
                return genMRID;
            }

            set
            {
                genMRID = value;
            }
        }
        private string genName;
        public string GenName
        {
            get
            {
                return genName;
            }

            set
            {
                genName = value;
            }
        }
        private string genWorkingMode;
        public string GenWorkingMode
        {
            get
            {
                return genWorkingMode;
            }

            set
            {
                genWorkingMode = value;
            }
        }
        private string genType;
        public string GenType
        {
            get
            {
                return genType;
            }

            set
            {
                genType = value;
            }
        }
        private string genSP;
        public string GenSP
        {
            get
            {
                return genSP;
            }

            set
            {
                genSP = value;
            }
        }
        private string genPrice;
        public string GenPrice
        {
            get
            {
                return genPrice;
            }

            set
            {
                genPrice = value;
            }
        }
        private string genAP;
        public string GenAP
        {
            get
            {
                return genAP;
            }

            set
            {
                genAP = value;
            }
        }
        private string genBP;
        public string GenBP
        {
            get
            {
                return genBP;
            }

            set
            {
                genBP = value;
            }
        }
        private string genHasMeas;
        public string GenHasMeas
        {
            get
            {
                return genHasMeas;
            }

            set
            {
                genHasMeas = value;
            }
        }
        private string genPMax;
        public string GenPMax
        {
            get
            {
                return genPMax;
            }

            set
            {
                genPMax = value;
            }
        }
        private string genPMin;
        public string GenPMin
        {
            get
            {
                return genPMin;
            }

            set
            {
                genPMin = value;
            }
        }
        #endregion

        #region AddWindowProperties
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string activePower;
        public string ActivePower
        {
            get
            {
                return activePower;
            }
            set
            {
                activePower = value;
                RaisePropertyChanged("ActivePower");
            }
        }

        private string basePoint;
        public string BasePoint
        {
            get
            {
                return basePoint;
            }
            set
            {
                basePoint = value;
                RaisePropertyChanged("BasePoint");
            }
        }

        private string setPoint;
        public string SetPoint
        {
            get
            {
                return setPoint;
            }
            set
            {
                setPoint = value;
                RaisePropertyChanged("SetPoint");
            }
        }

        private string pMin;
        public string PMin
        {
            get
            {
                return pMin;
            }
            set
            {
                pMin = value;
                RaisePropertyChanged("PMin");
            }
        }

        private string pMax;
        public string PMax
        {
            get
            {
                return pMax;
            }
            set
            {
                pMax = value;
                RaisePropertyChanged("PMax");
            }
        }

        private string price;
        public string Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                RaisePropertyChanged("Price");
            }
        }

        private string cmbHasMeasSelectedItem;

        public string CmbHasMeasSelectedItem
        {
            get
            {
                return cmbHasMeasSelectedItem;
            }
            set
            {
                cmbHasMeasSelectedItem = value;
                RaisePropertyChanged("CmbHasMeasSelectedItem");
            }
        }

        private string cmbGeneratorTypeSelectedItem;

        public string CmbGeneratorTypeSelectedItem
        {
            get
            {
                return cmbGeneratorTypeSelectedItem;
            }
            set
            {
                cmbGeneratorTypeSelectedItem = value;
                RaisePropertyChanged("CmbGeneratorTypeSelectedItem");
            }
        }

        private string cmbWorkingModeSelectedItem;

        public string CmbWorkingModeSelectedItem
        {
            get
            {
                return cmbWorkingModeSelectedItem;
            }
            set
            {
                cmbWorkingModeSelectedItem = value;
                RaisePropertyChanged("CmbWorkingModeSelectedItem");
            }
        }

        private bool radioButton;

        public bool RadioButton
        {
            get
            {
                return radioButton;
            }
            set
            {
                radioButton = value;
                RaisePropertyChanged("RadioButton");
            }
        }

        private bool radioButton1;

        public bool RadioButton1
        {
            get
            {
                return radioButton1;
            }
            set
            {
                radioButton1 = value;
                RaisePropertyChanged("RadioButton1");
            }
        }

        private bool radioButton2;

        public bool RadioButton2
        {
            get
            {
                return radioButton2;
            }
            set
            {
                radioButton2 = value;
                RaisePropertyChanged("RadioButton2");
            }
        }

        private string siteName;
        public string SiteName
        {
            get
            {
                return siteName;
            }
            set
            {
                siteName = value;
                RaisePropertyChanged("SiteName");
            }
        }

        private string groupName;
        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                groupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

        private string txbGroupName;
        public string TxbGroupName
        {
            get
            {
                return txbGroupName;
            }
            set
            {
                txbGroupName = value;
                RaisePropertyChanged("TxbGroupName");
            }
        }

        private string cmbSiteNameSelectedItem;

        public string CmbSiteNameSelectedItem
        {
            get
            {
                return cmbSiteNameSelectedItem;
            }
            set
            {
                cmbSiteNameSelectedItem = value;
                RaisePropertyChanged("CmbSiteNameSelectedItem");
            }
        }

        private string cmb2SiteNameSelectedItem;

        public string Cmb2SiteNameSelectedItem
        {
            get
            {
                return cmb2SiteNameSelectedItem;
            }
            set
            {
                cmb2SiteNameSelectedItem = value;
                RaisePropertyChanged("Cmb2SiteNameSelectedItem");
            }
        }

        private string cmb3SiteNameSelectedItem;

        public string Cmb3SiteNameSelectedItem
        {
            get
            {
                return cmb3SiteNameSelectedItem;
            }
            set
            {
                cmb3SiteNameSelectedItem = value;
                RaisePropertyChanged("Cmb3SiteNameSelectedItem");
            }
        }

        private Visibility txb8Visibility;
        public Visibility Txb8Visibility
        {
            get
            {
                return txb8Visibility;
            }
            set
            {
                txb8Visibility = value;
                RaisePropertyChanged("Txb8Visibility");
            }
        }

        private Visibility txb9Visibility;
        public Visibility Txb9Visibility
        {
            get
            {
                return txb9Visibility;
            }
            set
            {
                txb9Visibility = value;
                RaisePropertyChanged("Txb9Visibility");
            }
        }

        private Visibility cmbVisibility;
        public Visibility CmbVisibility
        {
            get
            {
                return cmbVisibility;
            }
            set
            {
                cmbVisibility = value;
                RaisePropertyChanged("CmbVisibility");
            }
        }

        private Visibility cmb2Visibility;
        public Visibility Cmb2Visibility
        {
            get
            {
                return cmb2Visibility;
            }
            set
            {
                cmb2Visibility = value;
                RaisePropertyChanged("Cmb2Visibility");
            }
        }

        private Visibility cmb3Visibility;
        public Visibility Cmb3Visibility
        {
            get
            {
                return cmb3Visibility;
            }
            set
            {
                cmb3Visibility = value;
                RaisePropertyChanged("Cmb3Visibility");
            }
        }

        private Visibility txbVisibility;
        public Visibility TxbVisibility
        {
            get
            {
                return txbVisibility;
            }
            set
            {
                txbVisibility = value;
                RaisePropertyChanged("TxbVisibility");
            }
        }

        #endregion

        #region EditWindowProperties
        private string editName;
        public string EditName
        {
            get
            {
                return editName;
            }
            set
            {
                editName = value;
                RaisePropertyChanged("EditName");
            }
        }

        private string editActivePower;
        public string EditActivePower
        {
            get
            {
                return editActivePower;
            }
            set
            {
                editActivePower = value;
                RaisePropertyChanged("EditActivePower");
            }
        }

        private string editBasePoint;
        public string EditBasePoint
        {
            get
            {
                return editBasePoint;
            }
            set
            {
                editBasePoint = value;
                RaisePropertyChanged("EditBasePoint");
            }
        }

        private string editSetPoint;
        public string EditSetPoint
        {
            get
            {
                return editSetPoint;
            }
            set
            {
                editSetPoint = value;
                RaisePropertyChanged("EditSetPoint");
            }
        }

        private string editPMin;
        public string EditPMin
        {
            get
            {
                return editPMin;
            }
            set
            {
                editPMin = value;
                RaisePropertyChanged("EditPMin");
            }
        }

        private string editPMax;
        public string EditPMax
        {
            get
            {
                return editPMax;
            }
            set
            {
                editPMax = value;
                RaisePropertyChanged("EditPMax");
            }
        }

        private string editPrice;
        public string EditPrice
        {
            get
            {
                return editPrice;
            }
            set
            {
                editPrice = value;
                RaisePropertyChanged("EditPrice");
            }
        }

        private string editCmbHasMeasSelectedItem;

        public string EditCmbHasMeasSelectedItem
        {
            get
            {
                return editCmbHasMeasSelectedItem;
            }
            set
            {
                editCmbHasMeasSelectedItem = value;
                RaisePropertyChanged("EditCmbHasMeasSelectedItem");
            }
        }

        private string editCmbGeneratorTypeSelectedItem;

        public string EditCmbGeneratorTypeSelectedItem
        {
            get
            {
                return editCmbGeneratorTypeSelectedItem;
            }
            set
            {
                editCmbGeneratorTypeSelectedItem = value;
                RaisePropertyChanged("EditCmbGeneratorTypeSelectedItem");
            }
        }

        private string editCmbWorkingModeSelectedItem;

        public string EditCmbWorkingModeSelectedItem
        {
            get
            {
                return editCmbWorkingModeSelectedItem;
            }
            set
            {
                editCmbWorkingModeSelectedItem = value;
                RaisePropertyChanged("EditCmbWorkingModeSelectedItem");
            }
        }

        private bool editRadioButton;

        public bool EditRadioButton
        {
            get
            {
                return editRadioButton;
            }
            set
            {
                editRadioButton = value;
                RaisePropertyChanged("EditRadioButton");
            }
        }

        private bool editRadioButton1;

        public bool EditRadioButton1
        {
            get
            {
                return editRadioButton1;
            }
            set
            {
                editRadioButton1 = value;
                RaisePropertyChanged("EditRadioButton1");
            }
        }

        private bool editRadioButton2;

        public bool EditRadioButton2
        {
            get
            {
                return editRadioButton2;
            }
            set
            {
                editRadioButton2 = value;
                RaisePropertyChanged("EditRadioButton2");
            }
        }

        private string editSiteName;
        public string EditSiteName
        {
            get
            {
                return editSiteName;
            }
            set
            {
                editSiteName = value;
                RaisePropertyChanged("EditSiteName");
            }
        }

        private string editGroupName;
        public string EditGroupName
        {
            get
            {
                return editGroupName;
            }
            set
            {
                editGroupName = value;
                RaisePropertyChanged("EditGroupName");
            }
        }

        private string editTxbGroupName;
        public string EditTxbGroupName
        {
            get
            {
                return editTxbGroupName;
            }
            set
            {
                editTxbGroupName = value;
                RaisePropertyChanged("EditTxbGroupName");
            }
        }

        private string editCmbSiteNameSelectedItem;

        public string EditCmbSiteNameSelectedItem
        {
            get
            {
                return editCmbSiteNameSelectedItem;
            }
            set
            {
                editCmbSiteNameSelectedItem = value;
                RaisePropertyChanged("EditCmbSiteNameSelectedItem");
            }
        }

        private string editCmb2SiteNameSelectedItem;

        public string EditCmb2SiteNameSelectedItem
        {
            get
            {
                return editCmb2SiteNameSelectedItem;
            }
            set
            {
                editCmb2SiteNameSelectedItem = value;
                RaisePropertyChanged("EditCmb2SiteNameSelectedItem");
            }
        }

        private string editCmb3SiteNameSelectedItem;

        public string EditCmb3SiteNameSelectedItem
        {
            get
            {
                return editCmb3SiteNameSelectedItem;
            }
            set
            {
                editCmb3SiteNameSelectedItem = value;
                RaisePropertyChanged("EditCmb3SiteNameSelectedItem");
            }
        }
        #endregion

        #region EditWindowProperties

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
                if (propName.Equals("Name"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("ActivePower"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("BasePoint"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("SetPoint"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("PMin"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("PMax"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("Price"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("CmbHasMeasSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("CmbGeneratorTypeSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("CmbWorkingModeSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                //else if (propName.Equals("RadioButton"))
                //{
                //    txb8Visibility = Visibility.Visible;
                //    txb9Visibility = Visibility.Visible;
                //    cmbVisibility = Visibility.Hidden;
                //    cmb2Visibility = Visibility.Hidden;
                //    cmb3Visibility = Visibility.Hidden;
                //    txbVisibility = Visibility.Hidden;
                //    CreateCommand.RaiseCanExecuteChanged();
                //}
                else if (propName.Equals("RadioButton1"))
                {
                    txb8Visibility = Visibility.Hidden;
                    txb9Visibility = Visibility.Hidden;
                    cmbVisibility = Visibility.Visible;
                    cmb2Visibility = Visibility.Visible;
                    cmb3Visibility = Visibility.Hidden;
                    txbVisibility = Visibility.Hidden;
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("RadioButton2"))
                {
                    txb8Visibility = Visibility.Hidden;
                    txb9Visibility = Visibility.Hidden;
                    cmbVisibility = Visibility.Hidden;
                    cmb2Visibility = Visibility.Hidden;
                    cmb3Visibility = Visibility.Visible;
                    txbVisibility = Visibility.Visible;
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("SiteName"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("GroupName"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("TxbGroupName"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("CmbSiteNameSelectedItem"))
                {
                    Site site = (Site)Enum.Parse(typeof(Site), cmbSiteNameSelectedItem);

                    Client.GroupNames.Clear();
                    foreach (Group group in Client.Groups)
                    {
                        if (site.MRID.Equals(group.SiteID))
                        {
                            Client.GroupNames.Add(group);
                        }
                    }
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("Cmb2SiteNameSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("Cmb3SiteNameSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                if (propName.Equals("EditName"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditActivePower"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditBasePoint"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditSetPoint"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditPMin"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditPMax"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditPrice"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditCmbHasMeasSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditCmbGeneratorTypeSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditCmbWorkingModeSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditRadioButton"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditRadioButton1"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditRadioButton2"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditSiteName"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditGroupName"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditTxbGroupName"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditCmbSiteNameSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditCmb2SiteNameSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("EditCmb3SiteNameSelectedItem"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("SelectedItem"))
                {
                    if (selectedItem != null)
                    {
                        generator = SelectedItem as Generator;
                        group = Client.GetGroupFromId(generator.GroupID);
                        site = Client.GetSiteFromId(group.SiteID);

                        GenMRID = generator.MRID;
                        GenName = generator.Name;
                        GenPMax = generator.Pmax.ToString();
                        GenPMin = generator.Pmin.ToString();
                        GenPrice = generator.Price.ToString();
                        GenSP = generator.SetPoint.ToString();
                        GenType = generator.GeneratorType.ToString();
                        GenAP = generator.ActivePower.ToString();
                        GenBP = generator.BasePoint.ToString();
                        GenWorkingMode = generator.WorkingMode.ToString();
                        GenHasMeas = generator.HasMeasurment.ToString();
                    }
                }
            }
        }

        private DelegateCommand createCommand;
        public DelegateCommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                {
                    createCommand = new DelegateCommand(CreateCommandAction, CanExecuteCreateCommand);
                }

                return createCommand;
            }
        }

        private DelegateCommand radioButtonn;
        public DelegateCommand RadioButtonn
        {
            get
            {
                if (radioButtonn == null)
                {
                    radioButtonn = new DelegateCommand(CreateButton, CanExecuteButtonCommand);
                }

                return radioButtonn;
            }
        }

        private void CreateButton()
        {
            radioButton = true;
            radioButton1 = false;
            radioButton2 = false;
            txb8Visibility = Visibility.Visible;
            txb9Visibility = Visibility.Visible;
            cmbVisibility = Visibility.Hidden;
            cmb2Visibility = Visibility.Hidden;
            cmb3Visibility = Visibility.Hidden;
            txbVisibility = Visibility.Hidden;
        }

        private bool CanExecuteButtonCommand()
        {
            return true;
        }

        private DelegateCommand radioButtonn1;
        public DelegateCommand RadioButtonn1
        {
            get
            {
                if (radioButtonn1 == null)
                {
                    radioButtonn1 = new DelegateCommand(CreateButton1, CanExecuteButtonCommand1);
                }

                return radioButtonn1;
            }
        }

        private void CreateButton1()
        {
            radioButton = false;
            radioButton1 = true;
            radioButton2 = false;
            txb8Visibility = Visibility.Hidden;
            txb9Visibility = Visibility.Hidden;
            cmbVisibility = Visibility.Visible;
            cmb2Visibility = Visibility.Visible;
            cmb3Visibility = Visibility.Hidden;
            txbVisibility = Visibility.Hidden;
        }

        private bool CanExecuteButtonCommand1()
        {
            return true;
        }

        private DelegateCommand radioButtonn2;
        public DelegateCommand RadioButtonn2
        {
            get
            {
                if (radioButtonn2 == null)
                {
                    radioButtonn2 = new DelegateCommand(CreateButton2, CanExecuteButtonCommand2);
                }

                return radioButtonn2;
            }
        }

        private void CreateButton2()
        {
            radioButton = false;
            radioButton1 = false;
            radioButton2 = true;
            txb8Visibility = Visibility.Hidden;
            txb9Visibility = Visibility.Hidden;
            cmbVisibility = Visibility.Hidden;
            cmb2Visibility = Visibility.Hidden;
            cmb3Visibility = Visibility.Visible;
            txbVisibility = Visibility.Visible;
        }

        private bool CanExecuteButtonCommand2()
        {
            return true;
        }

        private DelegateCommand editCommand;
        public DelegateCommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new DelegateCommand(EditCommandAction, CanExecuteEditCommand);
                }

                return editCommand;
            }
        }

        private DelegateCommand removeCommand;
        public DelegateCommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new DelegateCommand(RemoveCommandAction, CanExecuteRemoveCommand);
                }

                return removeCommand;
            }
        }

        private bool CanExecuteEditCommand()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return ValidationForCreateAndEdit();
        }

        private bool CanExecuteRemoveCommand()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private bool CanExecuteCreateCommand()
        {
            return ValidationForCreateAndEdit();
        }

        private bool ValidationForCreateAndEdit()
        {
            bool canExecute = true;
            canExecute = CheckStringInputField(Name);
            canExecute = CheckDoubleInputField(ActivePower);
            canExecute = CheckDoubleInputField(BasePoint);
            canExecute = CheckComboBoxInputField(CmbHasMeasSelectedItem);
            canExecute = CheckDoubleInputField(SetPoint);
            canExecute = CheckDoubleInputField(PMin);
            canExecute = CheckDoubleInputField(PMax);
            if (canExecute)
            {
                if (float.Parse(pMin) > float.Parse(pMax))
                {
                    canExecute = false;
                }
            }
            canExecute = CheckDoubleInputField(Price);
            canExecute = CheckComboBoxInputField(CmbGeneratorTypeSelectedItem);
            canExecute = CheckComboBoxInputField(CmbWorkingModeSelectedItem);

            if (radioButton)
            {
                if (!string.IsNullOrEmpty(SiteName) && !string.IsNullOrEmpty(groupName))
                {
                    site = new Site()
                    {
                        Name = this.SiteName
                    };

                    group = new Group()
                    {
                        Name = this.GroupName
                    };
                }
                else
                {
                    canExecute = CheckStringInputField(SiteName);
                    canExecute = CheckStringInputField(GroupName);
                }
            }
            if (radioButton1)
            {
                if (!string.IsNullOrEmpty(CmbSiteNameSelectedItem) && !string.IsNullOrEmpty(Cmb2SiteNameSelectedItem))
                {
                    site = null;
                    group = null;
                }
                else
                {
                    canExecute = CheckComboBoxInputField(CmbSiteNameSelectedItem);
                    canExecute = CheckComboBoxInputField(Cmb2SiteNameSelectedItem);
                }
            }
            else if (radioButton2)
            {
                if (!string.IsNullOrEmpty(Cmb3SiteNameSelectedItem) && !string.IsNullOrEmpty(TxbGroupName))
                {
                    site = null;
                    group = new Group()
                    {
                        Name = TxbGroupName
                    };
                }
                else
                {
                    canExecute = CheckComboBoxInputField(Cmb3SiteNameSelectedItem);
                    canExecute = CheckStringInputField(TxbGroupName);
                }
            }

            if (canExecute)
            {
                CreateInstanceOfGenerator();
            }
            
            return canExecute;
        }

        private void CreateInstanceOfGenerator()
        {
            bool hasMeas = this.cmbHasMeasSelectedItem.Equals("true") ? true : false;
            generator = new Generator()
            {
                ActivePower = float.Parse(this.ActivePower),
                BasePoint = float.Parse(this.BasePoint),
                GeneratorType = (GeneratorType)Enum.Parse(typeof(GeneratorType), this.CmbGeneratorTypeSelectedItem),
                HasMeasurment = hasMeas,
                Name = this.Name,
                Pmax = float.Parse(this.PMax),
                Pmin = float.Parse(this.PMin),
                Price = float.Parse(this.Price),
                SetPoint = float.Parse(this.SetPoint),
                WorkingMode = (WorkingMode)Enum.Parse(typeof(WorkingMode), this.CmbWorkingModeSelectedItem)
            };
        }

        private bool CheckStringInputField(string txb)
        {
            return !string.IsNullOrEmpty(txb);
        }

        private bool CheckDoubleInputField(string txb)
        {
            bool isOk = true;

            if (string.IsNullOrEmpty(txb))
            {
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(txb);

                    if (float.Parse(txb) < 1)
                    {
                        isOk = false;
                    }
                }
                catch
                {
                    isOk = false;
                }
            }

            return isOk;
        }

        private bool CheckComboBoxInputField(string cmb)
        {
            return !string.IsNullOrEmpty(cmb);
        }

        private void CreateCommandAction()
        {
            try
            {
                List<Generator> generators = new List<Generator>(1)
                {
                    generator
                };

                List<Site> sites = new List<Site>(1)
                {
                    site
                };

                List<Group> groups = new List<Group>(1)
                {
                    group
                };

                updateInfo = new UpdateInfo()
                {
                    Generators = generators,
                    Groups = groups,
                    Sites = sites,
                    UpdateType = UpdateType.ADD
                };

                Client.Command(updateInfo);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Needed power must be number.");
            }
        }

        private void EditCommandAction()
        {
            try
            {
                List<Generator> generators = new List<Generator>(1)
                {
                    generator
                };

                List<Site> sites = new List<Site>(1)
                {
                    site
                };

                List<Group> groups = new List<Group>(1)
                {
                    group
                };

                updateInfo = new UpdateInfo()
                {
                    Generators = generators,
                    Groups = groups,
                    Sites = sites,
                    UpdateType = UpdateType.UPDATE
                };
                Client.Command(updateInfo);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Needed power must be number.");
            }
        }

        private void RemoveCommandAction()
        {
            try
            {
                List<Generator> generators = new List<Generator>(1)
                {
                    generator
                };

                List<Site> sites = new List<Site>(1)
                {
                    site
                };

                List<Group> groups = new List<Group>(1)
                {
                    group
                };

                updateInfo = new UpdateInfo()
                {
                    Generators = generators,
                    Groups = groups,
                    Sites = sites,
                    UpdateType = UpdateType.REMOVE
                };

                Client.Command(updateInfo);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Needed power must be number.");
            }
        }
    }
}
