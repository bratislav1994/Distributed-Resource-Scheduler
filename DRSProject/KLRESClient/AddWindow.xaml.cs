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

            if (text_box1.Text.Trim().Equals(""))
            {
                text_box1.BorderBrush = Brushes.Red;
                text_box1.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (text_box2.Text.Trim().Equals(""))
            {
                text_box2.BorderBrush = Brushes.Red;
                text_box2.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (text_box3.Text.Trim().Equals(""))
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

            if (text_box4.Text.Trim().Equals(""))
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

            if (text_box10.Text.Trim().Equals(""))
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

            if (text_box5.Text.Trim().Equals(""))
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

            if (text_box6.Text.Trim().Equals(""))
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

            if (text_box7.Text.Trim().Equals(""))
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

            if (radioButton.IsChecked == true)
            {
                if (text_box8.Text.Trim().Equals(""))
                {
                    text_box8.BorderBrush = Brushes.Red;
                    text_box8.BorderThickness = new Thickness(1);
                    isOk = false;
                }

                if (text_box9.Text.Trim().Equals(""))
                {
                    text_box9.BorderBrush = Brushes.Red;
                    text_box9.BorderThickness = new Thickness(1);
                    isOk = false;
                }
            }
            else if (radioButton1.IsChecked == true)
            {

            }
            else if (radioButton2.IsChecked == true)
            {

            }

            if (isOk)
            {
                Generator newGen = new Generator()
                {
                    ActivePower = float.Parse(text_box3.Text.Trim()),
                    BasePoint = float.Parse(text_box4.Text.Trim()),
                    GeneratorType = (GeneratorType)combo_box2.SelectedItem,
                    HasMeasurment = (bool)combo_box1.SelectedItem,
                    MRID = text_box1.Text.Trim(),
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

        private void text_box1_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box1.BorderBrush = null;
            text_box1.BorderThickness = new Thickness(0);
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
            ComboBox cmb = this.FindName("cmb") as ComboBox;

            if (cmb != null)
            {
                cmb.Visibility = Visibility.Hidden;
                ComboBox cmb2 = this.FindName("cmb2") as ComboBox;
                cmb2.Visibility = Visibility.Hidden;
            }

            ComboBox cmb3 = this.FindName("cmb3") as ComboBox;

            if (cmb3 != null)
            {
                cmb3.Visibility = Visibility.Hidden;
                TextBox txb = this.FindName("txb") as TextBox;
                txb.Visibility = Visibility.Hidden;
            }

            text_box8.Visibility = Visibility.Visible;
            text_box9.Visibility = Visibility.Visible;
        }

        private void radioButton1_Click(object sender, RoutedEventArgs e)
        {
            text_box8.Visibility = Visibility.Hidden;
            text_box9.Visibility = Visibility.Hidden;

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

            ComboBox cmb3 = this.FindName("cmb3") as ComboBox;

            if (cmb3 != null)
            {
                cmb3.Visibility = Visibility.Hidden;
                TextBox txb = this.FindName("txb") as TextBox;
                txb.Visibility = Visibility.Hidden;
            }

            this.AddChild(cmb);
            this.AddChild(cmb2);
        }

        private void radioButton2_Click(object sender, RoutedEventArgs e)
        {
            text_box8.Visibility = Visibility.Hidden;
            text_box9.Visibility = Visibility.Hidden;

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

            ComboBox cmb = this.FindName("cmb") as ComboBox;

            if (cmb != null)
            {
                cmb.Visibility = Visibility.Hidden;
                ComboBox cmb2 = this.FindName("cmb2") as ComboBox;
                cmb2.Visibility = Visibility.Hidden;
            }

            this.AddChild(cmb3);
            this.AddChild(txb);
        }
    }
}
