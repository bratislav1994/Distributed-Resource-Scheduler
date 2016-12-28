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

            foreach(GeneratorType genType in Enum.GetValues(typeof(GeneratorType)))
            {
                combo_box2.Items.Add(genType);
            }

            foreach (WorkingMode workMode in Enum.GetValues(typeof(WorkingMode)))
            {
                combo_box3.Items.Add(workMode);
            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            bool isOk = true;

            if (string.IsNullOrEmpty(text_box2.Text.Trim()))
            {
                text_box2.BorderBrush = Brushes.Red;
                text_box2.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (string.IsNullOrEmpty(text_box3.Text.Trim()))
            {
                text_box3.BorderBrush = Brushes.Red;
                text_box3.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box3.Text.Trim());

                    if (float.Parse(text_box3.Text.Trim()) < 1)
                    {
                        text_box3.BorderBrush = Brushes.Red;
                        text_box3.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box3.BorderBrush = Brushes.Red;
                    text_box3.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }

            if (string.IsNullOrEmpty(text_box4.Text.Trim()))
            {
                text_box4.BorderBrush = Brushes.Red;
                text_box4.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box4.Text.Trim());

                    if (float.Parse(text_box4.Text.Trim()) < 1)
                    {
                        text_box4.BorderBrush = Brushes.Red;
                        text_box4.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box4.BorderBrush = Brushes.Red;
                    text_box4.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }

            if (combo_box1.SelectedItem == null)
            {
                combo_box1.BorderBrush = Brushes.Red;
                combo_box1.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (string.IsNullOrEmpty(text_box10.Text.Trim()))
            {
                text_box10.BorderBrush = Brushes.Red;
                text_box10.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box10.Text.Trim());

                    if (float.Parse(text_box10.Text.Trim()) < 1)
                    {
                        text_box10.BorderBrush = Brushes.Red;
                        text_box10.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box10.BorderBrush = Brushes.Red;
                    text_box10.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }

            if (string.IsNullOrEmpty(text_box5.Text.Trim()))
            {
                text_box5.BorderBrush = Brushes.Red;
                text_box5.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box5.Text.Trim());

                    if (float.Parse(text_box5.Text.Trim()) < 1)
                    {
                        text_box5.BorderBrush = Brushes.Red;
                        text_box5.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box5.BorderBrush = Brushes.Red;
                    text_box5.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }

            if (string.IsNullOrEmpty(text_box6.Text.Trim()))
            {
                text_box6.BorderBrush = Brushes.Red;
                text_box6.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box6.Text.Trim());

                    if (float.Parse(text_box6.Text.Trim()) < 1)
                    {
                        text_box6.BorderBrush = Brushes.Red;
                        text_box6.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box6.BorderBrush = Brushes.Red;
                    text_box6.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }

            if (string.IsNullOrEmpty(text_box7.Text.Trim()))
            {
                text_box7.BorderBrush = Brushes.Red;
                text_box7.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box7.Text.Trim());

                    if (float.Parse(text_box7.Text.Trim()) < 1)
                    {
                        text_box7.BorderBrush = Brushes.Red;
                        text_box7.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box7.BorderBrush = Brushes.Red;
                    text_box7.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }

            if (combo_box2.SelectedItem == null)
            {
                combo_box2.BorderBrush = Brushes.Red;
                combo_box2.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (combo_box3.SelectedItem == null)
            {
                combo_box3.BorderBrush = Brushes.Red;
                combo_box3.BorderThickness = new Thickness(1);
                isOk = false;
            }

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
                    if (string.IsNullOrEmpty(text_box8.Text.Trim()))
                    {
                        text_box8.BorderBrush = Brushes.Red;
                        text_box8.BorderThickness = new Thickness(1);
                        isOk = false;
                    }

                    if (string.IsNullOrEmpty(text_box9.Text.Trim()))
                    {
                        text_box9.BorderBrush = Brushes.Red;
                        text_box9.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
            }
            else if (radioButton1.IsChecked == true)
            {
                ComboBox cmb = this.FindName("cmb") as ComboBox;
                ComboBox cmb2 = this.FindName("cmb2") as ComboBox;

                if (cmb.SelectedItem != null && cmb2.SelectedItem != null)
                {
                    site = (Site)cmb.SelectedItem;
                    group = (Group)cmb2.SelectedItem;
                }
                else
                {
                    if (cmb.SelectedItem == null)
                    {
                        cmb.BorderBrush = Brushes.Red;
                        cmb.BorderThickness = new Thickness(1);
                        isOk = false;
                    }

                    if (cmb2.SelectedItem == null)
                    {
                        cmb2.BorderBrush = Brushes.Red;
                        cmb2.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
            }
            else if (radioButton2.IsChecked == true)
            {
                ComboBox cmb3 = this.FindName("cmb3") as ComboBox;
                TextBox txb = this.FindName("txb") as TextBox;

                if (cmb3.SelectedItem != null && string.IsNullOrEmpty(txb.Text.Trim()))
                {
                    site = (Site)cmb3.SelectedItem;
                    group = new Group()
                    {
                        Name = txb.Text.Trim()
                    };
                }
                else
                {
                    if (cmb3.SelectedItem == null)
                    {
                        cmb3.BorderBrush = Brushes.Red;
                        cmb3.BorderThickness = new Thickness(1);
                        isOk = false;
                    }

                    if (string.IsNullOrEmpty(txb.Text.Trim()))
                    {
                        txb.BorderBrush = Brushes.Red;
                        txb.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
            }

            if (isOk)
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
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void text_box2_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box2.BorderBrush = null;
            text_box2.BorderThickness = new Thickness(0);
        }

        private void text_box3_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box3.BorderBrush = null;
            text_box3.BorderThickness = new Thickness(0);
        }

        private void text_box4_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box4.BorderBrush = null;
            text_box4.BorderThickness = new Thickness(0);
        }

        private void combo_box1_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box1.BorderBrush = null;
            combo_box1.BorderThickness = new Thickness(0);
        }

        private void text_box5_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box5.BorderBrush = null;
            text_box5.BorderThickness = new Thickness(0);
        }

        private void text_box6_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box6.BorderBrush = null;
            text_box6.BorderThickness = new Thickness(0);
        }

        private void text_box7_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box7.BorderBrush = null;
            text_box7.BorderThickness = new Thickness(0);
        }

        private void combo_box2_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box2.BorderBrush = null;
            combo_box2.BorderThickness = new Thickness(0);
        }

        private void combo_box3_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box3.BorderBrush = null;
            combo_box3.BorderThickness = new Thickness(0);
        }

        private void text_box8_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box8.BorderBrush = null;
            text_box8.BorderThickness = new Thickness(0);
        }

        private void text_box9_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box9.BorderBrush = null;
            text_box9.BorderThickness = new Thickness(0);
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            //ComboBox cmb = this.FindName("cmb") as ComboBox;

            //if (cmb != null)
            //{
            //    cmb.Visibility = Visibility.Hidden;
            //    ComboBox cmb2 = this.FindName("cmb2") as ComboBox;
            //    cmb2.Visibility = Visibility.Hidden;
            //}

            //ComboBox cmb3 = this.FindName("cmb3") as ComboBox;

            //if (cmb3 != null)
            //{
            //    cmb3.Visibility = Visibility.Hidden;
            //    TextBox txb = this.FindName("txb") as TextBox;
            //    txb.Visibility = Visibility.Hidden;
            //}

            panel.Children.Clear();
            panel.Children.Add(text_box8);
            panel.Children.Add(text_box9);
        }

        private void radioButton1_Click(object sender, RoutedEventArgs e)
        {
            //text_box8.Visibility = Visibility.Hidden;
            //text_box9.Visibility = Visibility.Hidden;

            ComboBox cmb = new ComboBox()
            {
                Name = "cmb",
                Margin = text_box8.Margin,
                Width = text_box8.Width
            };

            ComboBox cmb2 = new ComboBox()
            {
                Name = "cmb2",
                Margin = text_box9.Margin,
                Width = text_box9.Width
            };

            //ComboBox cmb3 = this.FindName("cmb3") as ComboBox;

            //if (cmb3 != null)
            //{
            //    cmb3.Visibility = Visibility.Hidden;
            //    TextBox txb = this.FindName("txb") as TextBox;
            //    txb.Visibility = Visibility.Hidden;
            //}

            panel.Children.Clear();
            panel.Children.Add(cmb);
            panel.Children.Add(cmb2);
            
        }

        private void radioButton2_Click(object sender, RoutedEventArgs e)
        {
            //text_box8.Visibility = Visibility.Hidden;
            //text_box9.Visibility = Visibility.Hidden;

            ComboBox cmb3 = new ComboBox()
            {
                Name = "cmb3",
                Margin = text_box8.Margin,
                Width = text_box8.Width
            };
            
            TextBox txb = new TextBox()
            {
                Name = "txb",
                Height = text_box9.Height,
                Margin = text_box9.Margin,
                Width = text_box9.Width
            };

            //ComboBox cmb = this.FindName("cmb") as ComboBox;

            //if (cmb != null)
            //{
            //    cmb.Visibility = Visibility.Hidden;
            //    ComboBox cmb2 = this.FindName("cmb2") as ComboBox;
            //    cmb2.Visibility = Visibility.Hidden;
            //}

            panel.Children.Clear();
            panel.Children.Add(cmb3);
            panel.Children.Add(txb);
            
        }
    }
}
