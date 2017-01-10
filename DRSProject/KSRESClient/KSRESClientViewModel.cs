using CommonLibrary;
using CommonLibrary.Interfaces;
using Prism.Commands;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
                if(propName.Equals("CbSelectedItem"))
                {
                    Client.SetCurrentUser(CbSelectedItem);
                }
                else if(propName.Equals("NeededPower"))
                {
                    IssueCommand.RaiseCanExecuteChanged();
                }
                else if(propName.Equals("NumberOfDays"))
                {
                    DrawHistoryCommand.RaiseCanExecuteChanged();
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
            return !string.IsNullOrEmpty(NeededPower);
        }

        private void IssueCommandAction()
        {
            try
            {
                double np;
                bool isNumber = double.TryParse(NeededPower, out np);
                if(!isNumber)
                {
                    throw new Exception("Needed power must be number");
                }
                else if(np < 0)
                {
                    throw new Exception("Needed power must be positive");
                }
                Client.IssueCommand(np);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        #region Presentation
        private SortedDictionary<DateTime,double> productionHistory;
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

        private SortedDictionary<DateTime, double> loadForecast;
        public SortedDictionary<DateTime, double> LoadForecast
        {
            get
            {
                if(loadForecast == null)
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

        private String numberOfDays;
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

        private DelegateCommand drawHistoryCommand;
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

        private bool CanExecuteDrawHistoryCommand()
        {
            Double nod;

            return !string.IsNullOrEmpty(NumberOfDays) && (Double.TryParse(NumberOfDays, out nod));
        }

        private void DrawHistoryCommandAction()
        {
            try
            {
                double np;
                bool isNumber = double.TryParse(NumberOfDays,out np);
                if (!isNumber)
                {
                    throw new Exception("Number of days must be number");
                }
                else if(np < 0)
                {
                    throw new Exception("Number of days must be positive.");
                }

                //ProductionHistory = Client.GetProductionHistory(np);
                SortedDictionary<DateTime, double> test = new SortedDictionary<DateTime, double>();
                test.Add(DateTime.Now, 100);
                test.Add(DateTime.Now.AddMonths(1), 130);
                test.Add(DateTime.Now.AddMonths(2), 150);
                test.Add(DateTime.Now.AddMonths(3), 125);
                test.Add(DateTime.Now.AddMonths(4), 140);
                ProductionHistory = test;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DelegateCommand drawLoadForecastCommand;
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

        private DelegateCommand clearLoadForecast;
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

        private bool CanExecuteClearLoadForecastCommand()
        {
            return true;
        }

        private void ClearLoadForecastCommandAction()
        {
            LoadForecast = new SortedDictionary<DateTime, double>();
        }

        private DelegateCommand clearHistorycast;
        public DelegateCommand ClearHistorycast
        {
            get
            {
                if (clearHistorycast == null)
                {
                    clearHistorycast = new DelegateCommand(ClearHistoryCommandAction, CanExecuteClearHistoryCommand);
                }

                return clearHistorycast;
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
        #endregion Presentaion
    }
}
