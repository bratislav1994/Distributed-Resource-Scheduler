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
        private UpdateInfo getAllFromService;
        private ILKForClient proxy = null;

        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = ClientDatabase.Instance();
            LKClientService a = new LKClientService();
            DuplexChannelFactory<ILKForClient> factory = new DuplexChannelFactory<ILKForClient>(
                    new InstanceContext(a),
                        new NetTcpBinding(),
                        new EndpointAddress("net.tcp://localhost:5000/ILKForClient"));
            proxy = factory.CreateChannel();

            getAllFromService = proxy.GetMySystem();

            if (getAllFromService.Generators != null)
            {
                foreach (Generator gen in getAllFromService.Generators)
                {
                    ClientDatabase.Instance().Generators.Add(gen);
                }
            }

            dataGridGenerators.ItemsSource = ClientDatabase.Instance().Generators;

            if (getAllFromService.Groups != null)
            {
                foreach (Group group in getAllFromService.Groups)
                {
                    ClientDatabase.Instance().Groups.Add(group);
                }
            } 

            if (getAllFromService.Sites != null)
            {
                foreach (Site site in getAllFromService.Sites)
                {
                    ClientDatabase.Instance().Sites.Add(site);
                }
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
                Generator generator = (Generator)dataGridGenerators.SelectedItem;
                win1.text_box2.Text = generator.Name;
                win1.text_box3.Text = generator.ActivePower.ToString();
                win1.combo_box1.SelectedItem = generator.HasMeasurment.ToString();
                win1.text_box4.Text = generator.BasePoint.ToString();
                win1.text_box10.Text = generator.SetPoint.ToString();
                win1.text_box5.Text = generator.Pmin.ToString();
                win1.text_box6.Text = generator.Pmax.ToString();
                win1.text_box7.Text = generator.Price.ToString();
                win1.combo_box2.SelectedItem = generator.GeneratorType.ToString();
                win1.combo_box3.SelectedItem = generator.WorkingMode.ToString();
                foreach (Group group in ClientDatabase.Instance().Groups)
                {
                    if (group.MRID.Equals(generator.GroupID))
                    {
                        win1.cmb2.SelectedItem = group.Name.ToString();

                        foreach (Site site in ClientDatabase.Instance().Sites)
                        {
                            if (site.MRID.Equals(group.SiteID))
                            {
                                win1.cmb.SelectedItem = site.Name.ToString();
                                break;
                            }
                        }

                        break;
                    }
                }

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

                foreach (Group g in ClientDatabase.Instance().Groups)
                {
                    if (g.MRID.Equals(gen.GroupID))
                    {
                        bool findSite = false;
                        foreach (Site site in ClientDatabase.Instance().Sites)
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
