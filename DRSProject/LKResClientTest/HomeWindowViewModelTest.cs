// <copyright file="HomeWindowViewModelTest.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace LKResClientTest
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using CommonLibrary;
    using CommonLibrary.Interfaces;
    using KLRESClient;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Used for testing HomeWindowViewModel
    /// </summary>
    public class HomeWindowViewModelTest
    {
        /// <summary>
        /// instance of home window view model
        /// </summary>
        private HomeWindowViewModel homeVM = null;

        /// <summary>
        /// instance of client
        /// </summary>
        private LKClientService client = null;

        /// <summary>
        /// initialize fields
        /// </summary>
        [OneTimeSetUp]
        public void SetupTest()
        {
            this.client = new LKClientService() { DataContext = this };
            this.homeVM = new HomeWindowViewModel();
            this.homeVM.Client = this.client;
        }

        /// <summary>
        /// testing property
        /// </summary>
        [Test]
        public void PropertySetTest()
        {
            this.homeVM.Username = "test";
            this.homeVM.Username2 = "test";
            this.homeVM.Password = "password";
            this.homeVM.Password2 = "password";
            Assert.AreEqual("test", this.homeVM.Username);
            Assert.AreEqual("test", this.homeVM.Username2);
            Assert.AreEqual("password", this.homeVM.Password);
            Assert.AreEqual("password", this.homeVM.Password2);
        }

        /// <summary>
        /// testing if login command can be executed
        /// </summary>
        [Test]
        public void LoginCommandCanExecute()
        {
            this.homeVM.Username = "test";
            this.homeVM.Username2 = "test";
            this.homeVM.Password = "password";
            this.homeVM.Password2 = "password";
            Assert.IsTrue(this.homeVM.LoginCommand.CanExecute());
            this.homeVM.Password = string.Empty;
            Assert.IsFalse(this.homeVM.LoginCommand.CanExecute());
            this.homeVM.Password = "test";
            Assert.IsFalse(this.homeVM.LoginCommand.CanExecute());

            ILKForClient mockService = Substitute.For<ILKForClient>();
            mockService.Login("proba", "proba");
            mockService.GetMySystem().Returns(new UpdateInfo());

            this.homeVM.Client.Proxy = mockService;

            var t = new Thread(() =>
            {
                this.homeVM.HomeWin = new HomeWindow(this.homeVM.Client.DataContext);
                HomeWindow home = this.homeVM.HomeWin;
                this.homeVM.LoginCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            // catch
            ILKForClient mockService2 = Substitute.For<ILKForClient>();
            mockService.Login("p", "proba");
            this.homeVM.Client.Proxy = mockService2;
            this.homeVM.LoginCommand.Execute();
        }

        /// <summary>
        /// testing if register command can be executed
        /// </summary>
        [Test]
        public void RegistrateCommandCanExecute()
        {
            this.homeVM.Username = "test";
            this.homeVM.Username2 = "test";
            this.homeVM.Password = "password";
            this.homeVM.Password2 = "password";
            Assert.IsTrue(this.homeVM.RegistrateCommand.CanExecute());
            this.homeVM.Password2 = string.Empty;
            Assert.IsFalse(this.homeVM.RegistrateCommand.CanExecute());
            this.homeVM.Password2 = "test";
            Assert.IsFalse(this.homeVM.RegistrateCommand.CanExecute());

            ILKForClient mockService = Substitute.For<ILKForClient>();
            mockService.Registration("proba", "proba");
            mockService.GetMySystem().Returns(new UpdateInfo());

            this.homeVM.Client.Proxy = mockService;

            var t = new Thread(() =>
            {
                this.homeVM.HomeWin = new HomeWindow(this.homeVM.Client.DataContext);
                this.homeVM.RegistrateCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            // catch
            ILKForClient mockService2 = Substitute.For<ILKForClient>();
            mockService.Registration("p", "proba");
            this.homeVM.Client.Proxy = mockService2;
            this.homeVM.RegistrateCommand.Execute();
        }

        /// <summary>
        /// testing property changed
        /// </summary>
        [Test]
        public void PropertyChanged()
        {
            string receivedEvents = null;

            this.homeVM.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.homeVM.Username = "testing";
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("Username", receivedEvents);
        }

        /// <summary>
        /// testing binding of password box
        /// </summary>
        [Test]
        public void PasswordAsistant()
        {
            var t = new Thread(() =>
            {
                App app = new App();
                app.InitializeComponent();
                app.InitializeComponent();

                PasswordBox box = new PasswordBox();

                PasswordBoxAssistant.SetBoundPassword(box, "a");
                PasswordBoxAssistant.GetBindPassword(box);
                PasswordBoxAssistant.GetBoundPassword(box);
                box.Password = "test";

                box.PasswordChanged += delegate(object sender, RoutedEventArgs e) { };
                PasswordBoxAssistant.SetBindPassword(box, true);
                box.Password = "test";
                PasswordBoxAssistant.SetBindPassword(box, true);
                box.PasswordChanged += delegate(object sender, RoutedEventArgs e) { };
                PasswordBoxAssistant.SetBindPassword(box, true);
                PasswordBoxAssistant.SetBindPassword(box, true);

                box.Password += "e";
                PasswordBoxAssistant.SetBindPassword(box, false);
                box.PasswordChanged += delegate(object sender, RoutedEventArgs e) { };

                box.Password = null;
                PasswordBoxAssistant.SetBoundPassword(null, "a");
                box.PasswordChanged += delegate(object sender, RoutedEventArgs e) { };
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
    }
}
