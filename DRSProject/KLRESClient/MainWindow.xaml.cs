using CommonLibrary;
using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KLRESClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UpdateInfo getAllGenerators;
        private ILKForClient proxy = null;

        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = this;

            DuplexChannelFactory<ILKForClient> factory = new DuplexChannelFactory<ILKForClient>(
                    new InstanceContext(this),
                        new NetTcpBinding(),
                        new EndpointAddress("net.tcp://localhost:4000/ILKForClient"));
            proxy = factory.CreateChannel();

            getAllGenerators = proxy.GetMySystem();


            if (ClientDatabase.generators == null)
            {
                ClientDatabase.generators = new List<Generator>();
            }

            if (ClientDatabase.groups == null)
            {
                ClientDatabase.groups = new List<Group>();
            }

            if (ClientDatabase.sites == null)
            {
                ClientDatabase.sites = new List<Site>();
            }
        }

        private void AddGenerator(object sender, RoutedEventArgs e)
        {
            AddWindow win1 = new AddWindow();
            win1.Owner = this;
            win1.ShowDialog(); 
        }

        private void UpdateGenerator(object sender, RoutedEventArgs e)
        {
            if (dataGridGenerators.SelectedItem != null)
            {
                EditWindow win1 = new EditWindow();
                win1.Owner = this;
                win1.ShowDialog();
            }

            dataGridGenerators.SelectedItem = null;
        }

        private void RemoveGenerator(object sender, RoutedEventArgs e)
        {
            if (dataGridGenerators.SelectedItem != null)
            {
                DuplexChannelFactory<ILKClient> factory = new DuplexChannelFactory<ILKClient>(
                    new InstanceContext(this),
                    new NetTcpBinding(),
                    new EndpointAddress("net.tcp://localhost:4000/LKClientService"));
                ILKClient proxy = factory.CreateChannel();

                UpdateInfo updInfo = null;
                Generator gen = (Generator)dataGridGenerators.SelectedItem;

                foreach (Group g in ClientDatabase.groups)
                {
                    if (g.MRID.Equals(gen.GroupID))
                    {
                        bool findSite = false;
                        foreach (Site site in ClientDatabase.sites)
                        {
                            if (g.SiteID.Equals(site.MRID))
                            {
                                updInfo = new UpdateInfo()
                                {
                                    Generators = new List<Generator>()
                                    {
                                        gen
                                    },
                                    Groups = new List<Group>()
                                    {
                                        g
                                    },
                                    Sites = new List<Site>()
                                    {
                                        site
                                    },
                                    UpdateType = UpdateType.REMOVE
                                };

                                findSite = true;
                                break;
                            }
                        }

                        if (findSite)
                        {
                            break;
                        }
                    }
                }
                proxy.Update(updInfo);
            }

            dataGridGenerators.SelectedItem = null;
        }
    }
}
