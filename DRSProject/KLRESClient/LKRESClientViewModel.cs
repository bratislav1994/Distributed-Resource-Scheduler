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
                else if (propName.Equals("RadioButton"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("RadioButton1"))
                {
                    CreateCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("RadioButton2"))
                {
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
            return ValidationForCreateAndEdit();
        }

        private bool CanExecuteRemoveCommand()
        {
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
            return canExecute;
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
                if (updateInfo == null)
                {
                    throw new Exception();
                }
                updateInfo.UpdateType = UpdateType.ADD;
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
                if (updateInfo == null)
                {
                    throw new Exception();
                }
                updateInfo.UpdateType = UpdateType.UPDATE;
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
                if (updateInfo == null)
                {
                    throw new Exception();
                }
                updateInfo.UpdateType = UpdateType.REMOVE;
                Client.Command(updateInfo);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Needed power must be number.");
            }
        }
    }
}
