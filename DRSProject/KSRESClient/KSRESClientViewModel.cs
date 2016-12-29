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
            }
        }

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
    }
}
