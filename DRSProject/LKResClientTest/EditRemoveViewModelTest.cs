using CommonLibrary;
using CommonLibrary.Interfaces;
using KLRESClient;
using NSubstitute;
using NUnit.Framework;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Collections.Generic;


namespace LKResClientTest
{
    public class EditRemoveViewModelTest
    {
        private EditRemoveViewModel viewModel = null;
        private LKClientService client = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.client = new LKClientService() { DataContext = this };
            this.viewModel = new EditRemoveViewModel(this.client);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => this.viewModel = new EditRemoveViewModel(this.client));
            Assert.AreNotEqual(null, this.viewModel.Client);
        }

        [Test]
        public void ClientTest()
        {
            LKClientService client2 = new LKClientService();
            this.viewModel.Client = client2;
            Assert.AreEqual(client2, this.viewModel.Client);
        }

        [Test]
        public void SelectedItemTest()
        {
            //Assert.AreEqual(null, this.viewModel.SelectedItem);
            Generator selected = new Generator();
            this.viewModel.SelectedItem = selected;
            Assert.AreEqual(selected, this.viewModel.SelectedItem);
        }

        [Test]
        public void NameTest()
        {
            string name = "name";
            this.viewModel.EditName = name;
            Assert.AreEqual(name, this.viewModel.EditName);
        }

        [Test]
        public void ActivePowerTest()
        {
            string active = "50";
            this.viewModel.EditActivePower = active;
            Assert.AreEqual(active, this.viewModel.EditActivePower);
        }

        [Test]
        public void BasePointTest()
        {
            string basePoint = "10";
            this.viewModel.EditBasePoint = basePoint;
            Assert.AreEqual(basePoint, this.viewModel.EditBasePoint);
        }

        [Test]
        public void SetPointTest()
        {
            string setPoint = "10";
            this.viewModel.EditSetPoint = setPoint;
            Assert.AreEqual(setPoint, this.viewModel.EditSetPoint);
        }

        [Test]
        public void PMinTest()
        {
            string powerMin = "10";
            this.viewModel.EditPMin = powerMin;
            Assert.AreEqual(powerMin, this.viewModel.EditPMin);
        }

        [Test]
        public void PMaxTest()
        {
            string powerMax = "20";
            this.viewModel.EditPMax = powerMax;
            Assert.AreEqual(powerMax, this.viewModel.EditPMax);
        }

        [Test]
        public void PriceTest()
        {
            string price = "100";
            this.viewModel.EditPrice = price;
            Assert.AreEqual(price, this.viewModel.EditPrice);
        }

        [Test]
        public void CmbHasMeasSelectedItemTest()
        {
            bool hasMeasure = true;
            this.viewModel.EditCmbHasMeasSelectedItem = hasMeasure;
            Assert.AreEqual(hasMeasure, this.viewModel.EditCmbHasMeasSelectedItem);
        }

        [Test]
        public void CmbGeneratorTypeSelectedItemTest()
        {
            GeneratorType genType = GeneratorType.MICROHYDRO;
            this.viewModel.EditCmbGeneratorTypeSelectedItem = genType;
            Assert.AreEqual(genType, this.viewModel.EditCmbGeneratorTypeSelectedItem);
        }

        [Test]
        public void CmbWorkingModeSelectedItemTest()
        {
            WorkingMode workMode = WorkingMode.LOCAL;
            this.viewModel.EditCmbWorkingModeSelectedItem = workMode;
            Assert.AreEqual(workMode, this.viewModel.EditCmbWorkingModeSelectedItem);
        }

        [Test]
        public void RadioButtonTest()
        {
            bool radioButton = true;
            this.viewModel.EditRadioButton = radioButton;
            Assert.AreEqual(radioButton, this.viewModel.EditRadioButton);
        }

        [Test]
        public void RadioButton1Test()
        {
            bool radioButton = true;
            this.viewModel.EditRadioButton1 = radioButton;
            Assert.AreEqual(radioButton, this.viewModel.EditRadioButton1);
        }

        [Test]
        public void RadioButton2Test()
        {
            bool radioButton = true;
            this.viewModel.EditRadioButton2 = radioButton;
            Assert.AreEqual(radioButton, this.viewModel.EditRadioButton2);
        }

        [Test]
        public void SiteNameTest()
        {
            string site = "site";
            this.viewModel.EditSiteName = site;
            Assert.AreEqual(site, this.viewModel.EditSiteName);
        }

        [Test]
        public void GroupNameTest()
        {
            string group = "group";
            this.viewModel.EditGroupName = group;
            Assert.AreEqual(group, this.viewModel.EditGroupName);
        }

        [Test]
        public void TxbGroupNameTest()
        {
            string txb = "txb";
            this.viewModel.EditTxbGroupName = txb;
            Assert.AreEqual(txb, this.viewModel.EditTxbGroupName);
        }

        [Test]
        public void CmbSiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.viewModel.EditCmbSiteNameSelectedItem = site;
            Assert.AreEqual(site, this.viewModel.EditCmbSiteNameSelectedItem);
        }

        [Test]
        public void Cmb2GroupNameSelectedItemTest()
        {
            Group group = new Group();
            this.viewModel.EditCmb2GroupNameSelectedItem = group;
            Assert.AreEqual(group, this.viewModel.EditCmb2GroupNameSelectedItem);
        }

        [Test]
        public void Cmb3SiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.viewModel.EditCmb3SiteNameSelectedItem = site;
            Assert.AreEqual(site, this.viewModel.EditCmb3SiteNameSelectedItem);
        }

        [Test]
        public void Txb8VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditTxb8Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditTxb8Visibility);
        }

        [Test]
        public void Txb9VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditTxb9Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditTxb9Visibility);
        }

        [Test]
        public void CmbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditCmbVisibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditCmbVisibility);
        }

        [Test]
        public void Cmb2VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditCmb2Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditCmb2Visibility);
        }

        [Test]
        public void Cmb3VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditCmb3Visibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditCmb3Visibility);
        }

        [Test]
        public void TxbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.viewModel.EditTxbVisibility = visibility;
            Assert.AreEqual(visibility, this.viewModel.EditTxbVisibility);
        }

        [Test]
        public void RemoveCommandTest()
        {
            Generator g = new Generator() { MRID = "1", GroupID = "2" };
            this.viewModel.SelectedItem = g;

            this.viewModel.Client.Generators.Clear();
            this.viewModel.Client.Groups.Clear();
            this.viewModel.Client.Sites.Clear();


            this.viewModel.Client.Generators.Add(g);
            this.viewModel.Client.Groups.Add(new Group() { MRID = "2", SiteID = "3" });
            this.viewModel.Client.Sites.Add(new Site() { MRID = "3" });
            Assert.AreNotEqual(null, this.viewModel.RemoveCommand);
        }

        [Test]
        public void CancelCommandTest()
        {
            Assert.AreNotEqual(null, this.viewModel.EditCancelCommand);
        }

        [Test]
        public void EditCommandTest()
        {
            Assert.AreNotEqual(null, this.viewModel.EditCommand);
        }

        [Test]
        public void ClickEditCommandTest()
        {
            //EditRemoveViewModel target = new EditRemoveViewModel();
            //PrivateObject obj = new PrivateObject(target);
            //var retVal = obj.Invoke("PrivateMethod");
            //Assert.AreEqual(retVal);
            Assert.AreNotEqual(null, this.viewModel.ClickEditCommand);
        }
    }
}
