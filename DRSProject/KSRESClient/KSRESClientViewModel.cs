//-----------------------------------------------------------------------
// <copyright file="KSRESClientViewModel.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>View model which represents DataContext for main window class.</summary>
//-----------------------------------------------------------------------

namespace KSRESClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls.DataVisualization.Charting;
    using CommonLibrary;
    using CommonLibrary.Interfaces;
    using Prism.Commands;

    public class KSRESClientViewModel : INotifyPropertyChanged
    {
        private Client client;
        private String cbSelectedItem;
        private string neededPower;
        private DelegateCommand issueCommand;
        private SortedDictionary<DateTime, double> loadForecast;
        private object selectedItem;
        private string genMRID;
        private string genName;
        private string genWorkingMode;
        private string genType;
        private string genSP;
        private string genPrice;
        private string genAP;
        private string genBP;
        private string genHasMeas;
        private string genPMax;
        private string genPMin;
        private string genSite;
        private string genGroup;
        private SortedDictionary<DateTime, double> productionHistory;
        private String numberOfDays;
        private DelegateCommand drawHistoryCommand;
        private DelegateCommand drawLoadForecastCommand;
        private DelegateCommand clearLoadForecast;
        private DelegateCommand clearHistoryCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public Client Client
        {
            get
            {
                if (client == null)
                {
                    client = new Client();

                    DuplexChannelFactory<IKSForClient> factory = new DuplexChannelFactory<IKSForClient>(
                    new InstanceContext(client),
                        new NetTcpBinding(),
                        new EndpointAddress("net.tcp://localhost:10020/IKSForClient"));
                    client.Proxy = factory.CreateChannel();
                }

                return client;
            }

            set
            {
                client = value;
            }
        }

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
        
        public SortedDictionary<DateTime, double> ProductionHistory
        {
            get
            {
                if (productionHistory == null)
                {
                    productionHistory = new SortedDictionary<DateTime, double>();
                }

                return productionHistory;
            }

            set
            {
                productionHistory = value;
                RaisePropertyChanged("ProductionHistory");
            }
        }
        
        public SortedDictionary<DateTime, double> LoadForecast
        {
            get
            {
                if (loadForecast == null)
                {
                    loadForecast = new SortedDictionary<DateTime, double>();
                }

                return loadForecast;
            }

            set
            {
                loadForecast = value;
                RaisePropertyChanged("LoadForecast");
            }
        }
        
        public String NumberOfDays
        {
            get
            {
                return numberOfDays;
            }

            set
            {
                numberOfDays = value;
                RaisePropertyChanged("NumberOfDays");
            }
        }
        
        public DelegateCommand DrawHistoryCommand
        {
            get
            {
                if (drawHistoryCommand == null)
                {
                    drawHistoryCommand = new DelegateCommand(DrawHistoryCommandAction, CanExecuteDrawHistoryCommand);
                }

                return drawHistoryCommand;
            }
        }
        
        public DelegateCommand DrawLoadForecastCommand
        {
            get
            {
                if (drawLoadForecastCommand == null)
                {
                    drawLoadForecastCommand = new DelegateCommand(DrawLoadForecastCommandAction, CanExecuteDrawLoadForecastCommand);
                }

                return drawLoadForecastCommand;
            }
        }
        
        public DelegateCommand ClearLoadForecast
        {
            get
            {
                if (clearLoadForecast == null)
                {
                    clearLoadForecast = new DelegateCommand(ClearLoadForecastCommandAction, CanExecuteClearLoadForecastCommand);
                }

                return clearLoadForecast;
            }
        }
        
        public DelegateCommand ClearHistoryCommand
        {
            get
            {
                if (clearHistoryCommand == null)
                {
                    clearHistoryCommand = new DelegateCommand(ClearHistoryCommandAction, CanExecuteClearHistoryCommand);
                }

                return clearHistoryCommand;
            }
        }

        private bool CanExecuteClearHistoryCommand()
        {
            return true;
        }

        private void ClearHistoryCommandAction()
        {
            ProductionHistory = new SortedDictionary<DateTime, double>();
        }

        private bool CanExecuteIssueCommand()
        {
            return !string.IsNullOrEmpty(NeededPower);
        }

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
                if (propName.Equals("CbSelectedItem"))
                {
                    Client.SetCurrentUser(CbSelectedItem);
                }
                else if (propName.Equals("NeededPower"))
                {
                    IssueCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("NumberOfDays"))
                {
                    DrawHistoryCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("SelectedItem"))
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

        private void IssueCommandAction()
        {
            try
            {
                double np;
                bool isNumber = double.TryParse(NeededPower, out np);
                if (!isNumber)
                {
                    throw new Exception("Needed power must be number");
                }
                else if (np < 0)
                {
                    throw new Exception("Needed power must be positive");
                }

                Client.IssueCommand(Math.Round(np, 3));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanExecuteDrawHistoryCommand()
        {
            Double nod = 0;
            return !string.IsNullOrEmpty(NumberOfDays) && Double.TryParse(NumberOfDays, out nod) && nod > 0;
        }

        private void DrawHistoryCommandAction()
        {
            try
            {
                double np;
                bool isNumber = double.TryParse(NumberOfDays, out np);
                if (!isNumber)
                {
                    throw new Exception("Number of days must be number");
                }
                else if (np < 0)
                {
                    throw new Exception("Number of days must be positive.");
                }

                ProductionHistory = Client.GetProductionHistory(np);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanExecuteDrawLoadForecastCommand()
        {
            return true;
        }

        private void DrawLoadForecastCommandAction()
        {
            try
            {
                LoadForecast = Client.GetLoadForecast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanExecuteClearLoadForecastCommand()
        {
            return true;
        }

        private void ClearLoadForecastCommandAction()
        {
            LoadForecast = new SortedDictionary<DateTime, double>();
        }
    }
}
