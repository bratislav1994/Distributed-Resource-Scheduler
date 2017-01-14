// <copyright file="MasterViewModel.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace KLRESClient
{
    using System;
    using Prism.Commands;

    /// <summary>
    /// Contains sub view models
    /// </summary>
    public class MasterViewModel
    {
        #region fields

        /// <summary>
        /// Represent view model for add actions
        /// </summary>
        private AddWindowViewModel addWindowVM = null;

        /// <summary>
        /// Represent view model for edit and remove actions
        /// </summary>
        private EditRemoveViewModel editRemoveWindowVM = null;

        /// <summary>
        /// Represent instance of client
        /// </summary>
        private LKClientService client = null;

        /// <summary>
        /// Represent view model for login and register actions
        /// </summary>
        private HomeWindowViewModel homeVM = null;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterViewModel" /> class.
        /// </summary>
        /// <param name="win">main window</param>
        public MasterViewModel(MainWindow win)
        {
        }

        public MasterViewModel()
        {
            this.homeVM = new HomeWindowViewModel();
            this.Client = this.homeVM.Client;
            this.Client.DataContext = this;
            this.addWindowVM = new AddWindowViewModel(this.Client);
            this.editRemoveWindowVM = new EditRemoveViewModel(this.Client);
        }

        #endregion

        /// <summary>
        /// Gets or sets instance of client
        /// </summary>
        public LKClientService Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }


        /// <summary>
        /// Gets or sets view model for add action
        /// </summary>
        public AddWindowViewModel AddWindowVM
        {
            get
            {
                return this.addWindowVM;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.addWindowVM = value;
            }
        }

        /// <summary>
        /// Gets or sets view model for edit and remove
        /// </summary>
        public EditRemoveViewModel EditRemoveWindowVM
        {
            get
            {
                return this.editRemoveWindowVM;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.editRemoveWindowVM = value;
            }
        }

        /// <summary>
        /// Gets or sets view model for edit and remove
        /// </summary>
        public HomeWindowViewModel HomeVM
        {
            get
            {
                return this.homeVM;
            }

            set
            {
                this.homeVM = value;
            }
        }
    }
}
