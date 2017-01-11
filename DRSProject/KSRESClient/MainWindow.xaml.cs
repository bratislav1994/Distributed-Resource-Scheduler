//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Main window class.</summary>
//-----------------------------------------------------------------------

namespace KSRESClient
{
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
    using CommonLibrary;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KSRESClientViewModel model = new KSRESClientViewModel();

        public MainWindow()
        {
            DataContext = model;
            InitializeComponent();
        }

        private void GeneratorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GeneratorsDataGrid.SelectedItem != null)
            {
                DetailView detailView = new DetailView(model);
                detailView.ShowDialog();
            }
        }
    }
}
