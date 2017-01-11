//-----------------------------------------------------------------------
// <copyright file="DetailView.xaml.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Window for presentation of generotr details.</summary>
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
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for DetailView.xaml
    /// </summary>
    public partial class DetailView : Window
    {
        public DetailView(object model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}
