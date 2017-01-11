using CommonLibrary;
using CommonLibrary.Interfaces;
using KSRESClient;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRESClientTest
{
    [TestFixture]
    public class KSRESClientViewModelTest
    {
        private KSRESClientViewModel model;
        private Client client = null;
        private IKSForClient mockService;

        [OneTimeSetUp]
        public void Setup()
        {
            model = new KSRESClientViewModel();
        }

        [Test]
        public void ClientPropTest()
        {
            Client c = new Client();
            model.Client = c;

            Assert.AreEqual(c, model.Client);
            Assert.AreNotEqual(null, model.Client);
        }



        [Test]
        public void CbSelectedItemPropTest()
        {
            String selectedItem = "test";
            model.CbSelectedItem = selectedItem;

            Assert.AreEqual(selectedItem, model.CbSelectedItem);
            Assert.AreNotEqual(null, model.CbSelectedItem);
        }

        [Test]
        public void NeededPowerPropTest()
        {
            String neededPower = "test";
            model.NeededPower = neededPower;

            Assert.AreEqual(neededPower, model.NeededPower);
            Assert.AreNotEqual(null, model.NeededPower);
        }

        [Test]
        public void GenMRIDPropTest()
        {
            string genMRID = "test";
            model.GenMRID = genMRID;

            Assert.AreEqual(genMRID, model.GenMRID);
            Assert.AreNotEqual(null, model.GenMRID);
        }

        [Test]
        public void GenNamePropTest()
        {
            string genName = "test";
            model.GenName = genName;

            Assert.AreEqual(genName, model.GenName);
            Assert.AreNotEqual(null, model.GenName);
        }

        [Test]
        public void GenWorkingModePropTest()
        {
            string genWorkingMode = "test";
            model.GenWorkingMode = genWorkingMode;

            Assert.AreEqual(genWorkingMode, model.GenWorkingMode);
            Assert.AreNotEqual(null, model.GenWorkingMode);
        }

        [Test]
        public void GenTypePropTest()
        {
            string genType = "test";
            model.GenType = genType;

            Assert.AreEqual(genType, model.GenType);
            Assert.AreNotEqual(null, model.GenType);
        }

        [Test]
        public void GenSPPropTest()
        {
            string genSP = "test";
            model.GenSP = genSP;

            Assert.AreEqual(genSP, model.GenSP);
            Assert.AreNotEqual(null, model.GenSP);
        }

        [Test]
        public void GenPricePropTest()
        {
            string genPrice = "test";
            model.GenPrice = genPrice;

            Assert.AreEqual(genPrice, model.GenPrice);
            Assert.AreNotEqual(null, model.GenPrice);
        }

        [Test]
        public void GenAPPropTest()
        {
            string genAP = "test";
            model.GenAP = genAP;

            Assert.AreEqual(genAP, model.GenAP);
            Assert.AreNotEqual(null, model.GenAP);
        }

        [Test]
        public void GenBPPropTest()
        {
            string genBP = "test";
            model.GenBP = genBP;

            Assert.AreEqual(genBP, model.GenBP);
            Assert.AreNotEqual(null, model.GenBP);
        }

        [Test]
        public void GenHasMeasPropTest()
        {
            string genHasMeas = "test";
            model.GenHasMeas = genHasMeas;

            Assert.AreEqual(genHasMeas, model.GenHasMeas);
            Assert.AreNotEqual(null, model.GenHasMeas);
        }

        [Test]
        public void GenPMaxPropTest()
        {
            string genPMax = "test";
            model.GenPMax = genPMax;

            Assert.AreEqual(genPMax, model.GenPMax);
            Assert.AreNotEqual(null, model.GenPMax);
        }

        [Test]
        public void GenPMinPropTest()
        {
            string genPMin = "test";
            model.GenPMin = genPMin;

            Assert.AreEqual(genPMin, model.GenPMin);
            Assert.AreNotEqual(null, model.GenPMin);
        }

        [Test]
        public void GenSitePropTest()
        {
            string genSite = "test";
            model.GenSite = genSite;

            Assert.AreEqual(genSite, model.GenSite);
            Assert.AreNotEqual(null, model.GenSite);
        }

        [Test]
        public void GenGroupPropTest()
        {
            string genGroup = "test";
            model.GenGroup = genGroup;

            Assert.AreEqual(genGroup, model.GenGroup);
            Assert.AreNotEqual(null, model.GenGroup);
        }

        [Test]
        public void PropertyChanged()
        {
            string receivedEvents = null;

            this.model.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.model.CbSelectedItem = "testing";
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("CbSelectedItem", receivedEvents);
        }

        [Test]
        public void PropertyChanged_01()
        {
            string receivedEvents = null;

            this.model.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.model.NeededPower = "testing";
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("NeededPower", receivedEvents);
        }

        [Test]
        public void PropertyChanged_02()
        {
            string receivedEvents = null;

            Generator gen = new Generator();
            gen.MRID = "1";
            gen.ActivePower = 5;
            gen.BasePoint = 8;
            gen.GeneratorType = GeneratorType.WIND_STATIC;
            gen.GroupID = "1";
            gen.HasMeasurment = true;
            gen.Name = "a";
            gen.Pmax = 5;
            gen.Pmin = 1;
            gen.Price = 1;
            gen.SetPoint = 5;
            gen.WorkingMode = WorkingMode.REMOTE;

            Group group = new Group();
            group.MRID = "1";
            group.Name = "group";
            group.SiteID = "1";

            Site site = new Site();
            site.Name = "site";
            site.MRID = "1";

            client = new Client();

            UpdateInfo update = new UpdateInfo();
            update.Generators.Add(gen);
            update.Sites.Add(site);
            update.Groups.Add(group);

            client.Update(update, "temp");

            model.Client = client;

            this.model.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.model.SelectedItem = gen;
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("SelectedItem", receivedEvents);
        }

        [Test]
        public void IssueCommandTest()
        {
            mockService = Substitute.For<IKSForClient>();
            mockService.GetAllSystem().Returns(new List<LKResService>());

            client = new Client();
            client.Proxy = mockService;
            model.Client = client;
            model.NeededPower = String.Empty;
            model.CbSelectedItem = String.Empty;

            Assert.IsFalse(this.model.IssueCommand.CanExecute());

            model.NeededPower = "5"; 
            model.CbSelectedItem = "temp";

            Assert.IsTrue(this.model.IssueCommand.CanExecute());
            Assert.DoesNotThrow(() => this.model.IssueCommand.Execute());
        } 
    }
}
