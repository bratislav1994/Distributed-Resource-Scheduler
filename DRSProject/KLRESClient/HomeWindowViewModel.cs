using System;
using CommonLibrary.Interfaces;
using Prism.Commands;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;

namespace KLRESClient
{
    public class HomeWindowViewModel : INotifyPropertyChanged
    {
        private HomeWindow homeWin = null;

        /// <summary>
        /// Represent instance of client
        /// </summary>
        private LKClientService client = null;

        /// <summary>
        /// login command
        /// </summary>
        private DelegateCommand loginCommand;

        /// <summary>
        /// registrate command
        /// </summary>
        private DelegateCommand registrateCommand;

        private string username = string.Empty;
        private string password = string.Empty;
        private string username2 = string.Empty;
        private string password2 = string.Empty;

        public HomeWindowViewModel()
        {
            this.client = this.Client;
        }

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public LKClientService Client
        {
            get
            {
                if (this.client == null)
                {
                    this.client = new LKClientService() { DataContext = this };

                    DuplexChannelFactory<ILKForClient> factory = new DuplexChannelFactory<ILKForClient>(
                    new InstanceContext(this.client),
                        new NetTcpBinding(),
                        new EndpointAddress("net.tcp://localhost:5000/ILKForClient"));

                    this.client.Proxy = factory.CreateChannel();
                }

                return this.client;
            }

            set
            {
                this.client = value;
            }
        }

        public HomeWindow HomeWin
        {
            get
            {
                return this.homeWin;
            }

            set
            {
                this.homeWin = value;
            }
        }

        /// <summary>
        /// Gets or sets client username
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                this.username = value;
                this.RaisePropertyChanged("Username");
            }
        }

        /// <summary>
        /// Gets or sets client username
        /// </summary>
        public string Username2
        {
            get
            {
                return this.username2;
            }

            set
            {
                this.username2 = value;
                this.RaisePropertyChanged("Username2");
            }
        }

        /// <summary>
        /// Gets or sets client password
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// Gets or sets client password
        /// </summary>
        public string Password2
        {
            get
            {
                return this.password2;
            }

            set
            {
                this.password2 = value;
                this.RaisePropertyChanged("Password2");
            }
        }

        #endregion

        /// <summary>
        /// Gets information if login command can be executed
        /// </summary>
        public DelegateCommand LoginCommand
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new DelegateCommand(this.LoginCommandAction, this.CanLogin);
                }

                return this.loginCommand;
            }
        }

        private bool CanLogin()
        {
            if (!this.Client.CheckStringInputField(this.Username) || string.IsNullOrWhiteSpace(this.Username))
            {
                return false;
            }

            if (!this.Client.CheckStringInputField(this.Password) || string.IsNullOrWhiteSpace(this.Password))
            {
                return false;
            }

            if (this.Password.Length < 5)
            {
                return false;
            }

            return true;
        }

        private void LoginCommandAction()
        {
            try
            {
                this.Client.LogIn(this.Username, this.Password);
                this.homeWin.Close();
            }
            catch
            {
                MessageBox.Show("Error during login.");
            }
        }

        /// <summary>
        /// Gets information if registrate command can be executed
        /// </summary>
        public DelegateCommand RegistrateCommand
        {
            get
            {
                if (this.registrateCommand == null)
                {
                    this.registrateCommand = new DelegateCommand(this.RegistrateCommandAction, this.CanRegistrate);
                }

                return this.registrateCommand;
            }
        }

        private bool CanRegistrate()
        {
            if (!this.Client.CheckStringInputField(this.Username2) || string.IsNullOrWhiteSpace(this.Username2))
            {
                return false;
            }

            if (!this.Client.CheckStringInputField(this.Password2) || string.IsNullOrWhiteSpace(this.Password2))
            {
                return false;
            }

            if (this.Password2.Length < 5)
            {
                return false;
            }

            return true;
        }

        private void RegistrateCommandAction()
        {
            try
            {
                this.Client.Registration(this.Username2, this.Password2);
                this.homeWin.Close();
            }
            catch
            {
                MessageBox.Show("Error during registration.");
            }
        }

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

                if (propName.Equals("Username") || propName.Equals("Password"))
                {
                    this.LoginCommand.RaiseCanExecuteChanged();
                }
                else if (propName.Equals("Username2") || propName.Equals("Password2"))
                {
                    this.RegistrateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion
    }
}
