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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public ComboBox cmb = null;
        public ComboBox cmb2 = null;
        public ComboBox cmb3 = null;
        public TextBox txb = null;

        public EditWindow()
        {
            InitializeComponent();

            cmb = new ComboBox()
            {
                Name = "cmb",
                Margin = text_box8.Margin,
                Width = text_box8.Width,
            };

            cmb.SelectionChanged += new SelectionChangedEventHandler(Cmb_SelectionChanged);

            cmb2 = new ComboBox()
            {
                Name = "cmb2",
                Margin = text_box9.Margin,
                Width = text_box9.Width,
            };
            
            cmb3 = new ComboBox()
            {
                Name = "cmb3",
                Margin = text_box8.Margin,
                Width = text_box8.Width,
            };

            txb = new TextBox()
            {
                Name = "txb",
                Height = text_box9.Height,
                Margin = text_box9.Margin,
                Width = text_box9.Width,
            };

            panel.Children.Add(cmb);
            panel.Children.Add(cmb2);
            panel.Children.Add(cmb3);
            panel.Children.Add(txb);

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

            foreach (Group group in ClientDatabase.groups)
            {
                if (site.MRID.Equals(group.SiteID))
                {
                    cmb2.Items.Add(group);
                }
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            EditUpdateInfoInstance();
        }

        private void EditUpdateInfoInstance()
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
                EditAndSendUpdateInfoInstance(site, group);
            }
        }

        private void EditAndSendUpdateInfoInstance(Site site, Group group)
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
                UpdateType = UpdateType.UPDATE
            };

            DuplexChannelFactory<ILKClient> factory = new DuplexChannelFactory<ILKClient>(
                new InstanceContext(this),
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:4000/LKClientService"));
            ILKClient proxy = factory.CreateChannel();
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

        private void text_box2_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box2);
        }

        private void text_box3_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box3);
        }

        private void text_box4_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box4);
        }

        private void combo_box1_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBoxBorderBrush(combo_box1);
        }

        private void text_box5_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box5);
        }

        private void text_box6_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box6);
        }

        private void text_box7_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box7);
        }

        private void combo_box2_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBoxBorderBrush(combo_box2);
        }

        private void combo_box3_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBoxBorderBrush(combo_box3);
        }

        private void text_box8_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box8);
        }

        private void text_box9_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxBorderBrush(text_box9);
        }

        private void TextBoxBorderBrush(TextBox txb)
        {
            txb.BorderBrush = null;
            txb.BorderThickness = new Thickness(0);
        }

        private void ComboBoxBorderBrush(ComboBox cmb)
        {
            cmb.BorderBrush = null;
            cmb.BorderThickness = new Thickness(0);
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            ClickOnRadioButton();
        }

        private void ClickOnRadioButton()
        {
            panel.Children.Clear();
            panel.Children.Add(text_box8);
            panel.Children.Add(text_box9);
        }

        private void radioButton1_Click(object sender, RoutedEventArgs e)
        {
            ClickOnRadioButton1();
        }

        private void ClickOnRadioButton1()
        {
            panel.Children.Clear();
            panel.Children.Add(cmb);
            panel.Children.Add(cmb2);

            foreach (Site site in ClientDatabase.sites)
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
            panel.Children.Clear();
            panel.Children.Add(cmb3);
            panel.Children.Add(txb);

            foreach (Site site in ClientDatabase.sites)
            {
                cmb.Items.Add(site);
            }
        }
    }
}
