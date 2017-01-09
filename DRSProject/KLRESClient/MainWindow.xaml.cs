// <copyright file="MainWindow.xaml.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            MasterViewModel model = new MasterViewModel(this);
            this.DataContext = model;
            HomeWindow homeWin = new HomeWindow(this.DataContext);
            model.HomeVM.HomeWin = homeWin;
            homeWin.ShowDialog();
        }
    }
}
