// <copyright file="EditWindow.xaml.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for EditWindow
    /// </summary>
    public partial class EditWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditWindow" /> class.
        /// </summary>
        /// <param name="dataContext">data context from MainWindow</param>
        public EditWindow(object dataContext)
        {
            this.InitializeComponent();
            this.DataContext = dataContext;
        }
    }
}
