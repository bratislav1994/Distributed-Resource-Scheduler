// <copyright file="AddWindowViewModelTest.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace LKResClientTest
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using CommonLibrary;
    using CommonLibrary.Interfaces;
    using KLRESClient;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Used for testing AddWindowViewModel
    /// </summary>
    public class AddWindowViewModelTest
    {
        /// <summary>
        /// instance of add window view model
        /// </summary>
        private AddWindowViewModel addWindowVM = null;

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
            this.addWindowVM = new AddWindowViewModel(this.client);
            this.addWindowVM.IsTest = true;
        }

        /// <summary>
        /// testing constructor of add window view model
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => this.addWindowVM = new AddWindowViewModel(this.client));
            this.addWindowVM.IsTest = true;
            Assert.AreNotEqual(null, this.addWindowVM.Client);
        }

        /// <summary>
        /// test for get and set client
        /// </summary>
        [Test]
        public void ClientTest()
        {
            LKClientService client2 = new LKClientService();
            this.addWindowVM.Client = client2;
            Assert.AreEqual(client2, this.addWindowVM.Client);
        }

        /// <summary>
        /// test for get and set name
        /// </summary>
        [Test]
        public void NameTest()
        {
            string name = "name";
            this.addWindowVM.Name = name;
            Assert.AreEqual(name, this.addWindowVM.Name);
        }

        /// <summary>
        /// test for get and set active power
        /// </summary>
        [Test]
        public void ActivePowerTest()
        {
            string active = "50";
            this.addWindowVM.ActivePower = active;
            Assert.AreEqual(active, this.addWindowVM.ActivePower);
        }

        /// <summary>
        /// test for get and set power min
        /// </summary>
        [Test]
        public void PMinTest()
        {
            string powerMin = "10";
            this.addWindowVM.PMin = powerMin;
            Assert.AreEqual(powerMin, this.addWindowVM.PMin);
        }

        /// <summary>
        /// test for get and set power max
        /// </summary>
        [Test]
        public void PMaxTest()
        {
            string powerMax = "20";
            this.addWindowVM.PMax = powerMax;
            Assert.AreEqual(powerMax, this.addWindowVM.PMax);
        }

        /// <summary>
        /// test for get and set price
        /// </summary>
        [Test]
        public void PriceTest()
        {
            string price = "100";
            this.addWindowVM.Price = price;
            Assert.AreEqual(price, this.addWindowVM.Price);
        }

        /// <summary>
        /// test for get and set combo box
        /// </summary>
        [Test]
        public void CmbHasMeasSelectedItemTest()
        {
            bool hasMeasure = true;
            this.addWindowVM.CmbHasMeasSelectedItem = hasMeasure;
            Assert.AreEqual(hasMeasure, this.addWindowVM.CmbHasMeasSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box
        /// </summary>
        [Test]
        public void CmbGeneratorTypeSelectedItemTest()
        {
            GeneratorType genType = GeneratorType.MICROHYDRO;
            this.addWindowVM.CmbGeneratorTypeSelectedItem = genType;
            Assert.AreEqual(genType, this.addWindowVM.CmbGeneratorTypeSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box
        /// </summary>
        [Test]
        public void CmbWorkingModeSelectedItemTest()
        {
            WorkingMode workMode = WorkingMode.LOCAL;
            this.addWindowVM.CmbWorkingModeSelectedItem = workMode;
            Assert.AreEqual(workMode, this.addWindowVM.CmbWorkingModeSelectedItem);
        }

        /// <summary>
        /// test for get and set radio button
        /// </summary>
        [Test]
        public void RadioButtonTest()
        {
            bool radioButton = true;
            this.addWindowVM.RadioButton = radioButton;
            Assert.AreEqual(radioButton, this.addWindowVM.RadioButton);
        }

        /// <summary>
        /// test for get and set radio button
        /// </summary>
        [Test]
        public void RadioButton1Test()
        {
            bool radioButton = true;
            this.addWindowVM.RadioButton1 = radioButton;
            Assert.AreEqual(radioButton, this.addWindowVM.RadioButton1);
        }

        /// <summary>
        /// test for get and set radio button
        /// </summary>
        [Test]
        public void RadioButton2Test()
        {
            bool radioButton = true;
            this.addWindowVM.RadioButton2 = radioButton;
            Assert.AreEqual(radioButton, this.addWindowVM.RadioButton2);
        }

        /// <summary>
        /// test for get and set site name
        /// </summary>
        [Test]
        public void SiteNameTest()
        {
            string site = "site";
            this.addWindowVM.SiteName = site;
            Assert.AreEqual(site, this.addWindowVM.SiteName);
        }

        /// <summary>
        /// test for get and set group name
        /// </summary>
        [Test]
        public void GroupNameTest()
        {
            string group = "group";
            this.addWindowVM.GroupName = group;
            Assert.AreEqual(group, this.addWindowVM.GroupName);
        }

        /// <summary>
        /// test for get and set group name
        /// </summary>
        [Test]
        public void TxbGroupNameTest()
        {
            string txb = "txb";
            this.addWindowVM.TxbGroupName = txb;
            Assert.AreEqual(txb, this.addWindowVM.TxbGroupName);
        }

        /// <summary>
        /// test for get and set combo box items
        /// </summary>
        [Test]
        public void CmbSiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.addWindowVM.CmbSiteNameSelectedItem = site;
            Assert.AreEqual(site, this.addWindowVM.CmbSiteNameSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box selected item
        /// </summary>
        [Test]
        public void Cmb2GroupNameSelectedItemTest()
        {
            Group group = new Group();
            this.addWindowVM.Cmb2GroupNameSelectedItem = group;
            Assert.AreEqual(group, this.addWindowVM.Cmb2GroupNameSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box selected item
        /// </summary>
        [Test]
        public void Cmb3SiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.addWindowVM.Cmb3SiteNameSelectedItem = site;
            Assert.AreEqual(site, this.addWindowVM.Cmb3SiteNameSelectedItem);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Txb8VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Txb8Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Txb8Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Txb9VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Txb9Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Txb9Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void CmbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.CmbVisibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.CmbVisibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Cmb2VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Cmb2Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Cmb2Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Cmb3VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Cmb3Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Cmb3Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void TxbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.TxbVisibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.TxbVisibility);
        }

        /// <summary>
        /// test create command
        /// </summary>
        [Test]
        public void CreateCommandTest()
        {
            Assert.AreNotEqual(null, this.addWindowVM.CreateCommand);
        }

        /// <summary>
        /// test cancel command
        /// </summary>
        [Test]
        public void CancelCommandTest()
        {
            Assert.AreNotEqual(null, this.addWindowVM.CancelCommand);
        }

        /// <summary>
        /// test click add command
        /// </summary>
        [Test]
        public void ClickAddCommandTest()
        {
            Assert.AreNotEqual(null, this.addWindowVM.ClickAddCommand);
        }

        /// <summary>
        /// test for property changed
        /// </summary>
        [Test]
        public void PropertyChanged()
        {
            string receivedEvents = null;

            this.addWindowVM.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.addWindowVM.Name = "testing";
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("Name", receivedEvents);

            this.addWindowVM.Client.Groups.Clear();

            this.addWindowVM.Client.Groups.Add(new Group() { SiteID = "1" });
            this.addWindowVM.Client.Groups.Add(new Group() { SiteID = "2" });
            this.addWindowVM.CmbSiteNameSelectedItem = new Site() { MRID = "1" };
        }

        /// <summary>
        /// test for add click command action
        /// </summary>
        [Test]
        public void AddClickCommandActionTest()
        {
            Assert.IsTrue(this.addWindowVM.ClickAddCommand.CanExecute());

            var t = new Thread(() =>
            {
                this.addWindowVM.AddWin = new AddWindow(this.addWindowVM.Client.DataContext);
                AddWindow add = this.addWindowVM.AddWin;
                this.addWindowVM.ClickAddCommand.Execute();
                this.addWindowVM.AddWin.Close();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        /// <summary>
        /// test for cancel command action
        /// </summary>
        [Test]
        public void CancelCommandActionTest()
        {
            var t = new Thread(() =>
            {
                this.addWindowVM.AddWin = new AddWindow(this.addWindowVM.Client.DataContext);
                this.addWindowVM.CancelCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        /// <summary>
        /// test for create command action
        /// </summary>
        [Test]
        public void CreateCommandActionTest()
        {
            this.addWindowVM.Name = string.Empty;
            this.addWindowVM.ActivePower = "20";
            this.addWindowVM.PMin = "20";
            this.addWindowVM.PMax = "10";
            this.addWindowVM.Price = "20";
            this.addWindowVM.Price = "20";
            this.addWindowVM.SiteName = "test";
            this.addWindowVM.GroupName = "test";
            this.addWindowVM.RadioButton = true;

            Assert.IsFalse(this.addWindowVM.CreateCommand.CanExecute());

            this.addWindowVM.Name = "test";

            Assert.IsFalse(this.addWindowVM.CreateCommand.CanExecute());

            this.addWindowVM.SiteName = string.Empty;
            Assert.IsFalse(this.addWindowVM.CreateCommand.CanExecute());

            this.addWindowVM.SiteName = "test";
            this.addWindowVM.PMin = "10";
            this.addWindowVM.PMax = "20";
            this.addWindowVM.ActivePower = "25";
            Assert.IsFalse(this.addWindowVM.CreateCommand.CanExecute());

            this.addWindowVM.ActivePower = "15";
            Assert.IsTrue(this.addWindowVM.CreateCommand.CanExecute());
            this.addWindowVM.CreateCommand.Execute();

            this.addWindowVM.RadioButton = false;
            this.addWindowVM.RadioButton1 = true;

            this.addWindowVM.CmbSiteNameSelectedItem = null;
            this.addWindowVM.Cmb2GroupNameSelectedItem = new Group() { MRID = "1" };

            Assert.IsFalse(this.addWindowVM.CreateCommand.CanExecute());

            this.addWindowVM.CmbSiteNameSelectedItem = new Site();
            Assert.IsTrue(this.addWindowVM.CreateCommand.CanExecute());
            this.addWindowVM.CreateCommand.Execute();

            this.addWindowVM.RadioButton1 = false;
            this.addWindowVM.RadioButton2 = true;
            this.addWindowVM.TxbGroupName = string.Empty;
            this.addWindowVM.Cmb3SiteNameSelectedItem = new Site();

            Assert.IsFalse(this.addWindowVM.CreateCommand.CanExecute());

            this.addWindowVM.TxbGroupName = "test";
            Assert.IsTrue(this.addWindowVM.CreateCommand.CanExecute());

            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.ADD;
            update.Generators.Add(new Generator());
            update.Groups.Add(new Group());
            update.Sites.Add(new Site());

            ILKForClient mockService2 = Substitute.For<ILKForClient>();
            mockService2.Login("proba", "proba");
            mockService2.GetMySystem().Returns(update);
            mockService2.Update(update);

            this.client.Proxy = mockService2;
            this.client.LogIn("proba", "proba");
            this.client.Command(update);

            var t = new Thread(() =>
            {
                this.addWindowVM.AddWin = new AddWindow(this.addWindowVM.Client.DataContext);
                this.addWindowVM.CreateCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [Test]
        public void IsTestTest()
        {
            this.addWindowVM.IsTest = true;
            Assert.IsTrue(this.addWindowVM.IsTest);
        }
    }
}
