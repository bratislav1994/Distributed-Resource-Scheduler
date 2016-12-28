using CommonLibrary;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KLRESClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = this;
            
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

        }
    }
}
