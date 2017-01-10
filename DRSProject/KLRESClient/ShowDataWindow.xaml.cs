using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
// <copyright file="ShowDataWindow.xaml.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    /// <summary>
    /// Interaction logic for ShowDataWindow.xaml
    /// </summary>
    public partial class ShowDataWindow : Window
    {
        public ShowDataWindow(object dataContext)
        {
            InitializeComponent();
            this.DataContext = dataContext;
        }
    }
}
