using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommonLibrary;
using System.ServiceModel;
using CommonLibrary.Interfaces;

namespace KLRESClient
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {

        public AddWindow()
        {
            InitializeComponent();
            
            combo_box1.Items.Add(true);
            combo_box1.Items.Add(false);

            foreach (GeneratorType genType in Enum.GetValues(typeof(GeneratorType)))
            {
                combo_box2.Items.Add(genType);
            }

            foreach (WorkingMode workMode in Enum.GetValues(typeof(WorkingMode)))
            {
                combo_box3.Items.Add(workMode);
            }
        }

        private void Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbSelectionChanged();
        }

        private void CmbSelectionChanged()
        {
            Site site = (Site)cmb.SelectedItem;

            foreach (Group group in ClientDatabase.Instance().Groups)
            {
                if (site.MRID.Equals(group.SiteID))
                {
                    cmb2.Items.Add(group);
                }
            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            CreateUpdateInfoInstance();
        }

        private void CreateUpdateInfoInstance()
        {
            bool isOk = true;
            isOk = CheckStringInputField(text_box2);
            isOk = CheckDoubleInputField(text_box3);
            isOk = CheckDoubleInputField(text_box4);
            isOk = CheckComboBoxInputField(combo_box1);
            isOk = CheckDoubleInputField(text_box10);
            isOk = CheckDoubleInputField(text_box5);
            isOk = CheckDoubleInputField(text_box6);
            isOk = CheckDoubleInputField(text_box7);
            isOk = CheckComboBoxInputField(combo_box2);
            isOk = CheckComboBoxInputField(combo_box3);

            Site site = null;
            Group group = null;

            if (radioButton.IsChecked == true)
            {
                if (!string.IsNullOrEmpty(text_box8.Text.Trim()) && !string.IsNullOrEmpty(text_box9.Text.Trim()))
                {
                    site = new Site()
                    {
                        Name = text_box8.Text.Trim()
                    };

                    group = new Group()
                    {
                        Name = text_box9.Text.Trim()
                    };
                }
                else
                {
                    isOk = CheckStringInputField(text_box8);
                    isOk = CheckStringInputField(text_box9);
                }
            }
            else if (radioButton1.IsChecked == true)
            {
                if (cmb.SelectedItem != null && cmb2.SelectedItem != null)
                {
                    site = null;
                    group = null;
                }
                else
                {
                    isOk = CheckComboBoxInputField(cmb);
                    isOk = CheckComboBoxInputField(cmb2);
                }
            }
            else if (radioButton2.IsChecked == true)
            {
                if (cmb3.SelectedItem != null && string.IsNullOrEmpty(txb.Text.Trim()))
                {
                    site = null;
                    group = new Group()
                    {
                        Name = txb.Text.Trim()
                    };
                }
                else
                {
                    isOk = CheckComboBoxInputField(cmb3);
                    isOk = CheckStringInputField(txb);
                }
            }

            if (isOk)
            {
                CreateAndSendUpdateInfoInstance(site, group);
            }
        }

        private void CreateAndSendUpdateInfoInstance(Site site, Group group)
        {
            Generator newGen = new Generator()
            {
                ActivePower = float.Parse(text_box3.Text.Trim()),
                BasePoint = float.Parse(text_box4.Text.Trim()),
                GeneratorType = (GeneratorType)combo_box2.SelectedItem,
                HasMeasurment = (bool)combo_box1.SelectedItem,
                Name = text_box2.Text.Trim(),
                Pmax = float.Parse(text_box6.Text.Trim()),
                Pmin = float.Parse(text_box5.Text.Trim()),
                Price = float.Parse(text_box7.Text.Trim()),
                SetPoint = float.Parse(text_box10.Text.Trim()),
                WorkingMode = (WorkingMode)combo_box3.SelectedItem
            };

            UpdateInfo updInfo = new UpdateInfo()
            {
                Generators = new List<Generator>(1)
                    {
                        newGen
                    },
                Groups = new List<Group>(1)
                    {
                        group
                    },
                Sites = new List<Site>(1)
                    {
                        site
                    },
                UpdateType = UpdateType.ADD
            };

            DuplexChannelFactory<ILKForClient> factory = new DuplexChannelFactory<ILKForClient>(
                new InstanceContext(this),
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:5000/ILKForClient"));
            ILKForClient proxy = factory.CreateChannel();
            proxy.Update(updInfo);
            this.Close();
        }

        private bool CheckComboBoxInputField(ComboBox cmb)
        {
            bool isOk = true;

            if (cmb.SelectedItem == null)
            {
                cmb.BorderBrush = Brushes.Red;
                cmb.BorderThickness = new Thickness(1);
                isOk = false;
            }

            return isOk;
        }

        private bool CheckStringInputField(TextBox txb)
        {
            bool isOk = true;

            if (string.IsNullOrEmpty(txb.Text.Trim()))
            {
                txb.BorderBrush = Brushes.Red;
                txb.BorderThickness = new Thickness(1);
                isOk = false;
            }

            return isOk;
        }

        private bool CheckDoubleInputField(TextBox txb)
        {
            bool isOk = true;

            if (string.IsNullOrEmpty(txb.Text.Trim()))
            {
                txb.BorderBrush = Brushes.Red;
                txb.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(txb.Text.Trim());

                    if (float.Parse(txb.Text.Trim()) < 1)
                    {
                        txb.BorderBrush = Brushes.Red;
                        txb.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    txb.BorderBrush = Brushes.Red;
                    txb.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }

            return isOk;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            this.Close();
        }
        
        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            ClickOnRadioButton();
        }

        private void ClickOnRadioButton()
        {
            text_box8.Visibility = Visibility.Visible;
            text_box9.Visibility = Visibility.Visible;
            cmb.Visibility = Visibility.Hidden;
            cmb2.Visibility = Visibility.Hidden;
            cmb3.Visibility = Visibility.Hidden;
            txb.Visibility = Visibility.Hidden;
        }

        private void radioButton1_Click(object sender, RoutedEventArgs e)
        {
            ClickOnRadioButton1();
        }

        private void ClickOnRadioButton1()
        {
            text_box8.Visibility = Visibility.Hidden;
            text_box9.Visibility = Visibility.Hidden;
            cmb.Visibility = Visibility.Visible;
            cmb2.Visibility = Visibility.Visible;
            cmb3.Visibility = Visibility.Hidden;
            txb.Visibility = Visibility.Hidden;
            cmb.Items.Clear();

            foreach (Site site in ClientDatabase.Instance().Sites)
            {
                cmb.Items.Add(site);
            }
        }

        private void radioButton2_Click(object sender, RoutedEventArgs e)
        {
            ClickOnRadioButton2();
        }

        private void ClickOnRadioButton2()
        {
            text_box8.Visibility = Visibility.Hidden;
            text_box9.Visibility = Visibility.Hidden;
            cmb.Visibility = Visibility.Hidden;
            cmb2.Visibility = Visibility.Hidden;
            cmb3.Visibility = Visibility.Visible;
            txb.Visibility = Visibility.Visible;
            cmb3.Items.Clear();

            foreach (Site site in ClientDatabase.Instance().Sites)
            {
                cmb3.Items.Add(site);
            }
        }
    }
}
