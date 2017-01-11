// <copyright file="ShowDataWindow.xaml.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for ShowDataWindow
    /// </summary>
    public partial class ShowDataWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowDataWindow" /> class.
        /// </summary>
        /// <param name="dataContext">data context</param>
        public ShowDataWindow(object dataContext)
        {
            this.InitializeComponent();
            this.DataContext = dataContext;
        }
    }
}
