using CommonLibrary;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KSRESClient
{
    public class KSRESClientViewModel : INotifyPropertyChanged
    {
        private Client client;
        public Client Client
        {
            get
            {
                if (client == null)
                {
                    client = new Client();
                }
                return client;
            }
            set
            {
                client = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
                if(propName.Equals("CbSelectedItem"))
                {
                    Client.SetCurrentUser(CbSelectedItem);
                    IssueCommand.RaiseCanExecuteChanged();
                }
                else if(propName.Equals("NeededPower"))
                {
                    IssueCommand.RaiseCanExecuteChanged();
                }
                else if(propName.Equals("SelectedItem"))
                {
                    if (selectedItem != null)
                    {
                        Generator g = SelectedItem as Generator;
                        Group group = Client.GetGroupFromId(g.GroupID);
                        GenMRID = g.MRID;
                        GenName = g.Name;
                        GenPMax = g.Pmax.ToString();
                        GenPMin = g.Pmin.ToString();
                        GenPrice = g.Price.ToString();
                        GenGroup = Client.GetGroupNameFromId(g.GroupID);
                        GenSite = Client.GetSiteNameFromId(group.SiteID);
                        GenSP = g.SetPoint.ToString();
                        GenType = g.GeneratorType.ToString();
                        GenAP = g.ActivePower.ToString();
                        GenBP = g.BasePoint.ToString();
                        GenWorkingMode = g.WorkingMode.ToString();
                        GenHasMeas = g.HasMeasurment.ToString();
                    }
                }
            }
        }

        #region MainWindow
        private String cbSelectedItem;

        public String CbSelectedItem
        {
            get
            {
                return cbSelectedItem;
            }
            set
            {
                cbSelectedItem = value;
                RaisePropertyChanged("CbSelectedItem");
            }
        }

        private string neededPower;
        public string NeededPower
        {
            get
            {
                return neededPower;
            }
            set
            {
                neededPower = value;
                RaisePropertyChanged("NeededPower");

            }
        }

        private DelegateCommand issueCommand;
        public DelegateCommand IssueCommand
        {
            get
            {
                if (issueCommand == null)
                {
                    issueCommand = new DelegateCommand(IssueCommandAction, CanExecuteIssueCommand);
                }

                return issueCommand;
            }
        }
        
        private bool CanExecuteIssueCommand()
        {
            return (!string.IsNullOrEmpty(NeededPower) && NeededPower !="All") && (CbSelectedItem != null && CbSelectedItem != "All");
        }

        private void IssueCommandAction()
        {
            try
            {
                double np = double.Parse(NeededPower);
                if(np < 0)
                {
                    throw new Exception();
                }
                Client.IssueCommand(CbSelectedItem, np);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Needed power must be number.");
            }
        }
        #endregion MainWindow

        #region DetailView

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
        private string genSite;
        public string GenSite
        {
            get
            {
                return genSite;
            }

            set
            {
                genSite = value;
            }
        }
        private string genGroup;
        public string GenGroup
        {
            get
            {
                return genGroup;
            }

            set
            {
                genGroup = value;
            }
        }


        #endregion DetailView
    }
}
