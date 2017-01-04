// <copyright file="AddWindow.xaml.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for AddWindow
    /// </summary>
    public partial class AddWindow : System.Windows.Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddWindow" /> class.
        /// </summary>
        /// <param name="dataContext">data context from MainWindow</param>
        public AddWindow(object dataContext)
        {
            this.InitializeComponent();
            this.DataContext = dataContext;
        }
    }
}
