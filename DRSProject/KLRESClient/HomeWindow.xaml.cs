// <copyright file="HomeWindow.xaml.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for HomeWindow
    /// </summary>
    public partial class HomeWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeWindow" /> class.
        /// </summary>
        /// <param name="dataContext">data context from MainWindow</param>
        public HomeWindow(object dataContext)
        {
            this.InitializeComponent();
            this.DataContext = dataContext;
        }
    }
}
