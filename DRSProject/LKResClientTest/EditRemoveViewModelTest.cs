// <copyright file="EditRemoveViewModelTest.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace LKResClientTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using CommonLibrary;
    using CommonLibrary.Interfaces;
    using KLRESClient;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Used for testing EditRemoveViewModel
    /// </summary>
    public class EditRemoveViewModelTest
    {
        /// <summary>
        /// instance of edit remove view model
        /// </summary>
        private EditRemoveViewModel viewModel = null;

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
            this.viewModel = new EditRemoveViewModel(this.client);
        }

        /// <summary>
        /// testing constructor of edit remove view model
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => this.viewModel = new EditRemoveViewModel(this.client));
            Assert.AreNotEqual(null, this.viewModel.Client);
        }

        /// <summary>
        /// test for get and set client
        /// </summary>
        [Test]
        public void ClientTest()
        {
            LKClientService client2 = new LKClientService();
            this.viewModel.Client = client2;
            Assert.AreEqual(client2, this.viewModel.Client);
        }

        /// <summary>
        /// test for selected generator
        /// </summary>
        [Test]
        public void SelectedItemTest()
        {
            Generator g = new Generator() { MRID = "1", GroupID = "2" };
            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();
            this.viewModel.Client.Generators.Add(g);
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });

            this.viewModel.SelectedItem = g;
            Assert.AreEqual(g, this.viewModel.SelectedItem);
        }

        /// <summary>
        /// test for get and set name
        /// </summary>
        [Test]
        public void NameTest()
        {
            string name = "name";
            this.viewModel.EditName = name;
            Assert.AreEqual(name, this.viewModel.EditName);
        }

        /// <summary>
        /// test for get and set active power
        /// </summary>
        [Test]
        public void ActivePowerTest()
        {
            string active = "50";
            this.viewModel.EditActivePower = active;
            Assert.AreEqual(active, this.viewModel.EditActivePower);
        }

        /// <summary>
        /// test for get and set power min
        /// </summary>
        [Test]
        public void PMinTest()
        {
            string powerMin = "10";
            this.viewModel.EditPMin = powerMin;
            Assert.AreEqual(powerMin, this.viewModel.EditPMin);
        }

        /// <summary>
        /// test for get and set power max
        /// </summary>
        [Test]
        public void PMaxTest()
        {
            string powerMax = "20";
            this.viewModel.EditPMax = powerMax;
            Assert.AreEqual(powerMax, this.viewModel.EditPMax);
        }

        /// <summary>
        /// test for get and set price
        /// </summary>
        [Test]
        public void PriceTest()
        {
            string price = "100";
            this.viewModel.EditPrice = price;
            Assert.AreEqual(price, this.viewModel.EditPrice);
        }

        /// <summary>
        /// test for get and set combo box
        /// </summary>
        [Test]
        public void CmbHasMeasSelectedItemTest()
        {
            bool hasMeasure = true;
            this.viewModel.EditCmbHasMeasSelectedItem = hasMeasure;
            Assert.AreEqual(hasMeasure, this.viewModel.EditCmbHasMeasSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box
        /// </summary>
        [Test]
        public void CmbGeneratorTypeSelectedItemTest()
        {
            GeneratorType genType = GeneratorType.MICROHYDRO;
            this.viewModel.EditCmbGeneratorTypeSelectedItem = genType;
            Assert.AreEqual(genType, this.viewModel.EditCmbGeneratorTypeSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box
        /// </summary>
        [Test]
        public void CmbWorkingModeSelectedItemTest()
        {
            WorkingMode workMode = WorkingMode.LOCAL;
            this.viewModel.EditCmbWorkingModeSelectedItem = workMode;
            Assert.AreEqual(workMode, this.viewModel.EditCmbWorkingModeSelectedItem);
        }

        /// <summary>
        /// test for get and set radio button
        /// </summary>
        [Test]
        public void RadioButtonTest()
        {
            bool radioButton = true;
            this.viewModel.EditRadioButton = radioButton;
            Assert.AreEqual(radioButton, this.viewModel.EditRadioButton);
        }

        /// <summary>
        /// test for get and set radio button
        /// </summary>
        [Test]
        public void RadioButton1Test()
        {
            bool radioButton = true;
            this.viewModel.EditRadioButton1 = radioButton;
            Assert.AreEqual(radioButton, this.viewModel.EditRadioButton1);
        }

        /// <summary>
        /// test for get and set radio button
        /// </summary>
        [Test]
        public void RadioButton2Test()
        {
            bool radioButton = true;
            this.viewModel.EditRadioButton2 = radioButton;
            Assert.AreEqual(radioButton, this.viewModel.EditRadioButton2);
        }

        /// <summary>
        /// test for get and set site name
        /// </summary>
        [Test]
        public void SiteNameTest()
        {
            string site = "site";
            this.viewModel.EditSiteName = site;
            Assert.AreEqual(site, this.viewModel.EditSiteName);
        }

        /// <summary>
        /// test for get and set group name
        /// </summary>
        [Test]
        public void GroupNameTest()
        {
            string group = "group";
            this.viewModel.EditGroupName = group;
            Assert.AreEqual(group, this.viewModel.EditGroupName);
        }

        /// <summary>
        /// test for get and set group name
        /// </summary>
        [Test]
        public void TxbGroupNameTest()
        {
            string txb = "txb";
            this.viewModel.EditTxbGroupName = txb;
            Assert.AreEqual(txb, this.viewModel.EditTxbGroupName);
        }

        /// <summary>
        /// test for get and set combo box items
        /// </summary>
        [Test]
        public void CmbSiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.viewModel.EditCmbSiteNameSelectedItem = site;
            Assert.AreEqual(site, this.viewModel.EditCmbSiteNameSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box selected item
        /// </summary>
        [Test]
        public void Cmb2GroupNameSelectedItemTest()
        {
            Group group = new Group();
            this.viewModel.EditCmb2GroupNameSelectedItem = group;
            Assert.AreEqual(group, this.viewModel.EditCmb2GroupNameSelectedItem);
        }

        /// <summary>
        /// test for get and set combo box selected item
        /// </summary>
        [Test]
        public void Cmb3SiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.viewModel.EditCmb3SiteNameSelectedItem = site;
            Assert.AreEqual(site, this.viewModel.EditCmb3SiteNameSelectedItem);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Txb8VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditTxb8Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditTxb8Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Txb9VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditTxb9Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditTxb9Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void CmbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditCmbVisibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditCmbVisibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Cmb2VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditCmb2Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditCmb2Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void Cmb3VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditCmb3Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditCmb3Visibility);
        }

        /// <summary>
        /// test for get and set visibility of input field
        /// </summary>
        [Test]
        public void TxbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditTxbVisibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditTxbVisibility);
        }

        /// <summary>
        /// test remove command
        /// </summary>
        [Test]
        public void RemoveCommandTest()
        {
            Generator g = new Generator() { MRID = "1", GroupID = "2" };

            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();

            this.viewModel.Client.Generators.Add(g);
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });
            this.viewModel.SelectedItem = g;
            Assert.AreNotEqual(null, this.viewModel.RemoveCommand);
        }

        /// <summary>
        /// test cancel command
        /// </summary>
        [Test]
        public void CancelCommandTest()
        {
            Assert.AreNotEqual(null, this.viewModel.EditCancelCommand);
        }

        /// <summary>
        /// test edit command
        /// </summary>
        [Test]
        public void EditCommandTest()
        {
            Assert.AreNotEqual(null, this.viewModel.EditCommand);
        }

        /// <summary>
        /// test for click on edit button
        /// </summary>
        [Test]
        public void ClickEditCommandTest()
        {
            Assert.AreNotEqual(null, this.viewModel.ClickEditCommand);
        }

        /// <summary>
        /// test for property changed
        /// </summary>
        [Test]
        public void PropertyChanged()
        {
            string receivedEvents = null;

            this.viewModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.viewModel.EditName = "testing";
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("EditName", receivedEvents);

            this.viewModel.Client.EditGroupNames.Clear();

            this.viewModel.Client.Groups.Add(new Group() { SiteID = "1" });
            this.viewModel.Client.Groups.Add(new Group() { SiteID = "2" });
            this.viewModel.EditCmbSiteNameSelectedItem = new Site() { MRID = "1" };
        }

        /// <summary>
        /// test for edit click command action
        /// </summary>
        [Test]
        public void EditClickCommandActionTest()
        {
            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();

            EditRemoveViewModel temp = new EditRemoveViewModel(this.client);

            temp.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            temp.Client.Sites.Add(new Site() { MRID = "3" });

            Generator g = new Generator()
            {
                MRID = "test",
                GroupID = "2",
                Price = 50,
                Name = "test",
                ActivePower = 50,
                BasePoint = 20,
                GeneratorType = GeneratorType.MICROHYDRO,
                HasMeasurment = true,
                Pmax = 50,
                Pmin = 10,
                SetPoint = 20,
                WorkingMode = WorkingMode.LOCAL
            };

            temp.Client.Generators.Add(g);
            temp.SelectedItem = null;
            temp.SelectedItem = g;

            temp.EditName = string.Empty;
            temp.EditActivePower = string.Empty;

            // The dispatcher thread
            var t = new Thread(() =>
            {
                temp.EditWin = new EditWindow(temp.Client.DataContext);

                temp.ClickEditCommand.Execute();
                Assert.IsFalse(this.viewModel.ClickEditCommand.CanExecute());
            });

            //// Configure the thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        /// <summary>
        /// test for remove command action
        /// </summary>
        [Test]
        public void RemoveCommandActionTest()
        {
            Generator g = new Generator() { MRID = "1", GroupID = "2" };

            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();

            this.viewModel.Client.Generators.Add(g);
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });
            this.viewModel.SelectedItem = g;

            Assert.IsTrue(this.viewModel.RemoveCommand.CanExecute());
            this.viewModel.RemoveCommand.Execute();

            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();

            this.viewModel.Client.Generators.Add(new Generator() { MRID = "5", GroupID = "2" });
            this.viewModel.Client.Generators.Add(g);
            this.viewModel.Client.Generators.Add(new Generator() { MRID = "6", GroupID = "4" });
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });
            this.viewModel.SelectedItem = g;
            Assert.IsTrue(this.viewModel.RemoveCommand.CanExecute());
            this.viewModel.RemoveCommand.Execute();

            /////////////////////////
            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();

            this.viewModel.Client.Generators.Add(g);
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Groups.Add(new Group() { MRID = "5", SiteID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });
            this.viewModel.SelectedItem = g;
            Assert.IsTrue(this.viewModel.RemoveCommand.CanExecute());
            this.viewModel.RemoveCommand.Execute();

            // close bracket coveraged
            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();

            this.viewModel.Client.Generators.Add(new Generator() { MRID = "a", GroupID = "5" });
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });
            this.viewModel.SelectedItem = g;

            Assert.IsTrue(this.viewModel.RemoveCommand.CanExecute());
            this.viewModel.RemoveCommand.Execute();

            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();

            Generator gen = new Generator() { MRID = "a", GroupID = "2" };
            this.viewModel.Client.Generators.Add(gen);
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Groups.Add(new Group() { MRID = "5", SiteID = "4" });

            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "4" });
            this.viewModel.SelectedItem = gen;

            Assert.IsTrue(this.viewModel.RemoveCommand.CanExecute());
            this.viewModel.RemoveCommand.Execute();
        }

        /// <summary>
        /// test for edit command action
        /// </summary>
        [Test]
        public void EditCommandActionTest()
        {
            this.viewModel.EditName = string.Empty;
            this.viewModel.EditActivePower = "20";
            this.viewModel.EditPMin = "20";
            this.viewModel.EditPMax = "10";
            this.viewModel.EditPrice = "20";
            this.viewModel.EditPrice = "20";
            this.viewModel.EditSiteName = "test";
            this.viewModel.EditGroupName = "test";
            this.viewModel.EditRadioButton1 = false;
            this.viewModel.EditRadioButton = true;

            this.viewModel.SelectedItem = null;
            Assert.IsFalse(this.viewModel.EditCommand.CanExecute());

            this.viewModel.SelectedItem = new Generator();

            Assert.IsFalse(this.viewModel.EditCommand.CanExecute());

            this.viewModel.EditName = "test";

            Assert.IsFalse(this.viewModel.EditCommand.CanExecute());

            this.viewModel.EditSiteName = string.Empty;
            Assert.IsFalse(this.viewModel.EditCommand.CanExecute());

            this.viewModel.EditSiteName = "test";
            this.viewModel.EditPMin = "10";
            this.viewModel.EditPMax = "20";
            this.viewModel.EditActivePower = "25";
            Assert.IsFalse(this.viewModel.EditCommand.CanExecute());

            this.viewModel.EditActivePower = "15";
            Assert.IsTrue(this.viewModel.EditCommand.CanExecute());
            this.viewModel.EditCommand.Execute();
            this.FillInputFields();
            this.viewModel.EditRadioButton = false;
            this.viewModel.EditRadioButton1 = true;

            this.viewModel.EditCmbSiteNameSelectedItem = null;
            this.viewModel.EditCmb2GroupNameSelectedItem = new Group() { MRID = "1" };

            Assert.IsFalse(this.viewModel.EditCommand.CanExecute());

            this.viewModel.EditCmbSiteNameSelectedItem = new Site();
            Assert.IsTrue(this.viewModel.EditCommand.CanExecute());
            this.viewModel.EditCommand.Execute();
            this.FillInputFields();

            this.viewModel.EditRadioButton1 = false;
            this.viewModel.EditRadioButton2 = true;
            this.viewModel.EditTxbGroupName = string.Empty;
            this.viewModel.EditCmb3SiteNameSelectedItem = new Site();

            Assert.IsFalse(this.viewModel.EditCommand.CanExecute());

            this.viewModel.EditTxbGroupName = "test";
            Assert.IsTrue(this.viewModel.EditCommand.CanExecute());

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
                this.viewModel.EditWin = new EditWindow(this.viewModel.Client.DataContext);
                this.viewModel.EditWin = this.viewModel.EditWin;
                this.viewModel.EditCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        /// <summary>
        /// help method
        /// </summary>
        public void FillInputFields()
        {
            this.viewModel.EditActivePower = "20";
            this.viewModel.EditPrice = "20";
            this.viewModel.EditPrice = "20";
            this.viewModel.EditSiteName = "test";
            this.viewModel.EditGroupName = "test";
            this.viewModel.SelectedItem = new Generator();
            this.viewModel.EditName = "test";
            this.viewModel.EditPMin = "10";
            this.viewModel.EditPMax = "20";
        }

        /// <summary>
        /// Test for data history property
        /// </summary>
        [Test]
        public void DataHistoryTest()
        {
            List<KeyValuePair<DateTime, double>> history = new List<KeyValuePair<DateTime, double>>();
            this.viewModel.DataHistory = history;
            Assert.AreEqual(this.viewModel.DataHistory, history);
            this.viewModel.DataHistory = null;
            history.Add(new KeyValuePair<DateTime, double>(DateTime.Now, 20));
            
            Assert.AreNotEqual(this.viewModel.DataHistory, history);
        }

        /// <summary>
        /// Test for all history property
        /// </summary>
        [Test]
        public void AllHistoryTest()
        {
            string history = "test";
            this.viewModel.AllHistory = history;
            Assert.AreEqual(this.viewModel.AllHistory, history);
        }

        /// <summary>
        /// Test method about execution show data
        /// </summary>
        [Test]
        public void ShowDataExecuteTest()
        {
            this.viewModel.SelectedItem = null;
            Assert.IsFalse(this.viewModel.ShowDataCommand.CanExecute());

            this.client.Generators.Clear();
            this.client.Groups.Clear();
            this.client.Sites.Clear();

            Generator g = new Generator() { MRID = "1", GroupID = "2", HasMeasurment = true };
            Group group = new Group() { MRID = "2", SiteID = "3" };
            Site site = new Site() { MRID = "3" };

            this.client.Generators.Add(g);
            this.client.Groups.Add(group);
            this.client.Sites.Add(site);
            this.viewModel.SelectedItem = g;
            
            Assert.IsTrue(this.viewModel.ShowDataCommand.CanExecute());
            this.viewModel.ShowDataCommand.Execute();

            ILKForClient mockService = Substitute.For<ILKForClient>();
            List<KeyValuePair<DateTime, double>> temp = new List<KeyValuePair<DateTime, double>>();
            temp.Add(new KeyValuePair<DateTime, double>(DateTime.Now, 20));

            mockService.GetMeasurements("1").Returns(temp);
            this.viewModel.Client.Proxy = mockService;
            this.viewModel.SelectedItem = g;
            this.viewModel.ShowDataCommand.Execute();

            this.viewModel.SelectedItem = g;

            for (int i = 0; i < 10; i++)
            {
                temp.Add(new KeyValuePair<DateTime, double>(DateTime.Now.AddDays(1 * i), i * 10));
            }

            var t = new Thread(() =>
            {
                this.viewModel.ShowWin = new ShowDataWindow(this.client.DataContext);
                this.viewModel.ShowDataCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        /// <summary>
        /// test for exit command
        /// </summary>
        [Test]
        public void ExitCommandTest()
        {
            Assert.AreNotEqual(null, this.viewModel.ExitCommand);
        }

        /// <summary>
        /// test for exit command action, when variable execute is true
        /// </summary>
        [Test]
        public void ExitCommandAction()
        {
            ShowDataWindow window = null;

            var t = new Thread(() =>
            {
                window = new ShowDataWindow(this.client.DataContext);
                Assert.AreNotEqual(window, this.viewModel.ShowWin);
                this.viewModel.ShowWin = new ShowDataWindow(this.client.DataContext);
                this.viewModel.ExitCommand.Execute();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
    }
}
