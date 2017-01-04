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
    public class AddWindowViewModelTest
    {
        private AddWindowViewModel addWindowVM = null;
        private LKClientService client = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.client = new LKClientService() { DataContext = this };
            this.addWindowVM = new AddWindowViewModel(this.client);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => this.addWindowVM = new AddWindowViewModel(this.client));
            Assert.AreNotEqual(null, this.addWindowVM.Client);
        }

        [Test]
        public void ClientTest()
        {
            LKClientService client2 = new LKClientService();
            this.addWindowVM.Client = client2;
            Assert.AreEqual(client2, this.addWindowVM.Client);
        }

        [Test]
        public void NameTest()
        {
            string name = "name";
            this.addWindowVM.Name = name;
            Assert.AreEqual(name, this.addWindowVM.Name);
        }

        [Test]
        public void ActivePowerTest()
        {
            string active = "50";
            this.addWindowVM.ActivePower = active;
            Assert.AreEqual(active, this.addWindowVM.ActivePower);
        }

        [Test]
        public void BasePointTest()
        {
            string basePoint = "10";
            this.addWindowVM.BasePoint = basePoint;
            Assert.AreEqual(basePoint, this.addWindowVM.BasePoint);
        }

        [Test]
        public void SetPointTest()
        {
            string setPoint = "10";
            this.addWindowVM.SetPoint = setPoint;
            Assert.AreEqual(setPoint, this.addWindowVM.SetPoint);
        }

        [Test]
        public void PMinTest()
        {
            string powerMin = "10";
            this.addWindowVM.PMin = powerMin;
            Assert.AreEqual(powerMin, this.addWindowVM.PMin);
        }

        [Test]
        public void PMaxTest()
        {
            string powerMax = "20";
            this.addWindowVM.PMax = powerMax;
            Assert.AreEqual(powerMax, this.addWindowVM.PMax);
        }

        [Test]
        public void PriceTest()
        {
            string price = "100";
            this.addWindowVM.Price = price;
            Assert.AreEqual(price, this.addWindowVM.Price);
        }

        [Test]
        public void CmbHasMeasSelectedItemTest()
        {
            bool hasMeasure = true;
            this.addWindowVM.CmbHasMeasSelectedItem = hasMeasure;
            Assert.AreEqual(hasMeasure, this.addWindowVM.CmbHasMeasSelectedItem);
        }

        [Test]
        public void CmbGeneratorTypeSelectedItemTest()
        {
            GeneratorType genType = GeneratorType.MICROHYDRO;
            this.addWindowVM.CmbGeneratorTypeSelectedItem = genType;
            Assert.AreEqual(genType, this.addWindowVM.CmbGeneratorTypeSelectedItem);
        }

        [Test]
        public void CmbWorkingModeSelectedItemTest()
        {
            WorkingMode workMode = WorkingMode.LOCAL;
            this.addWindowVM.CmbWorkingModeSelectedItem = workMode;
            Assert.AreEqual(workMode, this.addWindowVM.CmbWorkingModeSelectedItem);
        }

        [Test]
        public void RadioButtonTest()
        {
            bool radioButton = true;
            this.addWindowVM.RadioButton = radioButton;
            Assert.AreEqual(radioButton, this.addWindowVM.RadioButton);
        }

        [Test]
        public void RadioButton1Test()
        {
            bool radioButton = true;
            this.addWindowVM.RadioButton1 = radioButton;
            Assert.AreEqual(radioButton, this.addWindowVM.RadioButton1);
        }

        [Test]
        public void RadioButton2Test()
        {
            bool radioButton = true;
            this.addWindowVM.RadioButton2 = radioButton;
            Assert.AreEqual(radioButton, this.addWindowVM.RadioButton2);
        }

        [Test]
        public void SiteNameTest()
        {
            string site = "site";
            this.addWindowVM.SiteName = site;
            Assert.AreEqual(site, this.addWindowVM.SiteName);
        }

        [Test]
        public void GroupNameTest()
        {
            string group = "group";
            this.addWindowVM.GroupName = group;
            Assert.AreEqual(group, this.addWindowVM.GroupName);
        }

        [Test]
        public void TxbGroupNameTest()
        {
            string txb = "txb";
            this.addWindowVM.TxbGroupName = txb;
            Assert.AreEqual(txb, this.addWindowVM.TxbGroupName);
        }

        [Test]
        public void CmbSiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.addWindowVM.CmbSiteNameSelectedItem = site;
            Assert.AreEqual(site, this.addWindowVM.CmbSiteNameSelectedItem);
        }

        [Test]
        public void Cmb2GroupNameSelectedItemTest()
        {
            Group group = new Group();
            this.addWindowVM.Cmb2GroupNameSelectedItem = group;
            Assert.AreEqual(group, this.addWindowVM.Cmb2GroupNameSelectedItem);
        }

        [Test]
        public void Cmb3SiteNameSelectedItemTest()
        {
            Site site = new Site();
            this.addWindowVM.Cmb3SiteNameSelectedItem = site;
            Assert.AreEqual(site, this.addWindowVM.Cmb3SiteNameSelectedItem);
        }

        [Test]
        public void Txb8VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Txb8Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Txb8Visibility);
        }

        [Test]
        public void Txb9VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Txb9Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Txb9Visibility);
        }

        [Test]
        public void CmbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.CmbVisibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.CmbVisibility);
        }

        [Test]
        public void Cmb2VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Cmb2Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Cmb2Visibility);
        }

        [Test]
        public void Cmb3VisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.Cmb3Visibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.Cmb3Visibility);
        }

        [Test]
        public void TxbVisibilityTest()
        {
            Visibility visibility = Visibility.Visible;
            this.addWindowVM.TxbVisibility = visibility;
            Assert.AreEqual(visibility, this.addWindowVM.TxbVisibility);
        }

        [Test]
        public void CreateCommandTest()
        {
            Assert.AreNotEqual(null, this.addWindowVM.CreateCommand);
        }

        [Test]
        public void CancelCommandTest()
        {
            Assert.AreNotEqual(null, this.addWindowVM.CancelCommand);
        }

        [Test]
        public void ClickAddCommandTest()
        {
            Assert.AreNotEqual(null, this.addWindowVM.ClickAddCommand);
        }
    }
}
