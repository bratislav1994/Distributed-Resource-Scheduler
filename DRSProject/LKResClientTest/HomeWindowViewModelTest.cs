using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLRESClient;
using NUnit.Framework;
using System.ComponentModel;
using CommonLibrary;
using CommonLibrary.Interfaces;
using NSubstitute;
using System.Threading;
using Prism.Commands;
using System.Windows;
using System.Windows.Controls;

namespace LKResClientTest
{
    public class HomeWindowViewModelTest
    {
        private HomeWindowViewModel homeVM = null;
        private LKClientService client = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.client = new LKClientService() { DataContext = this };
            this.homeVM = new HomeWindowViewModel();
            this.homeVM.Client = this.client;
        }

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

        [Test]
        public void PropertyChanged()
        {
            string receivedEvents = null;

            this.homeVM.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.homeVM.Username = "testing";
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("Username", receivedEvents);

            var t = new Thread(() =>
            {
                PasswordBox box = new PasswordBox();
                bool wascalled = false;

                var execute = new DelegateCommand(() => { wascalled = true; });

                PasswordBoxAssistant.SetBoundPassword(box, "a");
                
                PasswordBoxAssistant.SetBindPassword(box, true);
                PasswordBoxAssistant.GetBindPassword(box);
                PasswordBoxAssistant.GetBoundPassword(box);
                box.Password = "test";
                box.PasswordChanged += delegate (object sender, RoutedEventArgs e) {};
                PasswordBoxAssistant.SetBoundPassword(null, "a");
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            

            //AssertIsTrue(wascalled);
        }
    }
}
