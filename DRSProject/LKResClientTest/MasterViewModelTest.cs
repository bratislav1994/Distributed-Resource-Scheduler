// <copyright file="MasterViewModelTest.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

using NSubstitute;

namespace LKResClientTest
{
    using System;
    using System.Threading;
    using KLRESClient;
    using NUnit.Framework;

    /// <summary>
    /// Used for testing MasterViewModel
    /// </summary>
    [TestFixture]
    public class MasterViewModelTest
    {
        #region Declarations

        /// <summary>
        /// instance of master view model
        /// </summary>
        private MasterViewModel masterVM;

        /// <summary>
        /// Represent instance of client
        /// </summary>
        private LKClientService client = null;

        /// <summary>
        /// Use for closing MainWindow
        /// </summary>
        private MainWindow mainWindow = null;

        private LKClientService mockClient = null;

        #endregion

        /// <summary>
        /// initialize attributes
        /// </summary>
        [OneTimeSetUp]
        public void SetupTest()
        {
            MainWindow window = null;

            var t = new Thread(() =>
            {
                window = new MainWindow();
                this.masterVM = new MasterViewModel();
                mockClient = Substitute.For<LKClientService>();
                this.masterVM.Client = mockClient;
                this.masterVM.AddWindowVM = new AddWindowViewModel(this.masterVM.Client);
                this.masterVM.EditRemoveWindowVM = new EditRemoveViewModel(this.masterVM.Client);
                this.masterVM.MainWin = window;
                this.masterVM.HomeVM = new HomeWindowViewModel();
                // this.masterVM = new MasterViewModel(window);
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        /// <summary>
        /// test for constructor
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => this.masterVM = new MasterViewModel());
            Assert.AreEqual(null, this.masterVM.AddWindowVM);
            Assert.AreEqual(null, this.masterVM.EditRemoveWindowVM);
            Assert.AreEqual(null, this.masterVM.Client);
        }

        /// <summary>
        /// test for client
        /// </summary>
        [Test]
        public void ClientTest()
        {
            this.masterVM = new MasterViewModel();
            this.masterVM.Client = mockClient;
            // Assert.AreNotEqual(null, this.masterVM.Client);
            LKClientService client2 = new LKClientService();
            this.masterVM.Client = client2;
            Assert.AreEqual(client2, this.masterVM.Client);
        }

        /// <summary>
        /// test for home window view model
        /// </summary>
        [Test]
        public void HomeWindowVMTest()
        {
            Assert.AreEqual(null, this.masterVM.HomeVM);
            HomeWindowViewModel home = new HomeWindowViewModel(this.client);
            this.masterVM.HomeVM = home;
            Assert.AreEqual(home, this.masterVM.HomeVM);
        }

        /// <summary>
        /// test for add window view model
        /// </summary>
        [Test]
        public void AddWindowVMTest()
        {
            this.masterVM = new MasterViewModel();
            this.masterVM.AddWindowVM = new AddWindowViewModel(this.masterVM.Client);
            Assert.AreNotEqual(null, this.masterVM.AddWindowVM);
            Assert.Throws<ArgumentNullException>(() => this.masterVM.AddWindowVM = null);
            AddWindowViewModel add = new AddWindowViewModel(this.client);
            this.masterVM.AddWindowVM = add;
            Assert.AreEqual(add, this.masterVM.AddWindowVM);
        }

        /// <summary>
        /// test for edit remove view model
        /// </summary>
        [Test]
        public void EditRemoveWindowVMTest()
        {
            this.masterVM = new MasterViewModel();
            this.masterVM.EditRemoveWindowVM = new EditRemoveViewModel(this.masterVM.Client);
            Assert.AreNotEqual(null, this.masterVM.EditRemoveWindowVM);
            Assert.Throws<ArgumentNullException>(() => this.masterVM.EditRemoveWindowVM = null);
            EditRemoveViewModel viewModel = new EditRemoveViewModel(this.client);
            this.masterVM.EditRemoveWindowVM = viewModel;
            Assert.AreEqual(viewModel, this.masterVM.EditRemoveWindowVM);
        }

        /// <summary>
        /// test for exit command
        /// </summary>
        [Test]
        public void ExitCommandTest()
        {
            Assert.AreNotEqual(null, this.masterVM.ExitCommand);
        }

        /// <summary>
        /// test for exit command action, when variable execute is true
        /// </summary>
        [Test]
        public void ExitCommandAction()
        {
            MainWindow window = null;

            var t = new Thread(() =>
            {
                window = new MainWindow();
                this.masterVM = new MasterViewModel(window);
                this.masterVM.ExitCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
    }
}
