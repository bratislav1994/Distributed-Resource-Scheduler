using CommonLibrary;
using CommonLibrary.Interfaces;
using KSRes.Data;
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
        public void NumberOfDaysPropTest()
        {
            string numberOfDays = "5";
            model.NumberOfDays = numberOfDays;

            Assert.AreEqual(numberOfDays, model.NumberOfDays);
            Assert.AreNotEqual(null, model.NumberOfDays);
        }

        [Test]
        public void ProductionHistoryPropTest()
        {
            model.ProductionHistory = null;

            SortedDictionary<DateTime, double> test = new SortedDictionary<DateTime, double>();
            for (int i = 0; i < 5; i++)
            {
                test.Add(DateTime.Now.AddMinutes(i), i);
            }
            model.ProductionHistory = test;

            Assert.AreEqual(test.Count, model.ProductionHistory.Count);

            Assert.IsTrue(test.SequenceEqual(model.ProductionHistory));
        }

        [Test]
        public void LoadForecastPropTest()
        {
            model.LoadForecast = null;

            SortedDictionary<DateTime, double> test = new SortedDictionary<DateTime, double>();
            for (int i = 0; i < 5; i++)
            {
                test.Add(DateTime.Now.AddMinutes(i), i);
            }
            model.LoadForecast = test;

            Assert.AreEqual(test.Count, model.LoadForecast.Count);

            Assert.IsTrue(test.SequenceEqual(model.LoadForecast));
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

            Assert.IsFalse(this.model.IssueCommand.CanExecute());

            model.NeededPower = "5"; 

            Assert.IsTrue(this.model.IssueCommand.CanExecute());
            Assert.DoesNotThrow(() => this.model.IssueCommand.Execute());
        }

        [Test]
        public void DrawHistoryCommandTest()
        {
            mockService = Substitute.For<IKSForClient>();
            mockService.GetAllSystem().Returns(new List<LKResService>());

            client = new Client();
            client.Proxy = mockService;
            model.Client = client;
            model.NumberOfDays = String.Empty;

            Assert.IsFalse(this.model.DrawHistoryCommand.CanExecute());

            model.NumberOfDays = "-5";

            Assert.IsFalse(this.model.DrawHistoryCommand.CanExecute());
            this.model.DrawHistoryCommand.Execute();

            model.NumberOfDays = "error";

            Assert.IsFalse(this.model.DrawHistoryCommand.CanExecute());
            this.model.DrawHistoryCommand.Execute();

            model.NumberOfDays = "5";

            Assert.IsTrue(this.model.DrawHistoryCommand.CanExecute());
            this.model.DrawHistoryCommand.Execute();
        }

        [Test]
        public void ClearDrawHistoryCommandTest()
        {
            mockService = Substitute.For<IKSForClient>();
            mockService.GetAllSystem().Returns(new List<LKResService>());
            SortedDictionary<DateTime, double> test = new SortedDictionary<DateTime, double>();

            for (int i = 0; i < 5; i++)
            {
                test.Add(DateTime.Now.AddMinutes(i), i);
            }

            client = new Client();
            client.Proxy = mockService;
            model.Client = client;
            model.Client.Proxy.GetProductionHistory(Arg.Any<double>()).Returns(test);
            model.NumberOfDays = "5";
            
            model.DrawHistoryCommand.Execute();

            Assert.AreNotEqual(0, model.ProductionHistory.Count);

            model.ClearHistoryCommand.Execute();

            Assert.AreEqual(0, model.ProductionHistory.Count);
        }

        [Test]
        public void DrawLoadForeCastCommandTest()
        {
            mockService = Substitute.For<IKSForClient>();
            mockService.GetAllSystem().Returns(new List<LKResService>());

            SortedDictionary<DateTime, double> test = new SortedDictionary<DateTime, double>();

            for (int i = 0; i < 5; i++)
            {
                test.Add(DateTime.Now.AddMinutes(i), i);
            }

            client = new Client();
            client.Proxy = mockService;
            model.Client = client;
            model.Client.Proxy.GetLoadForecast().Returns(test);
            
            this.model.DrawLoadForecastCommand.Execute();

            Assert.IsTrue(test.SequenceEqual(model.LoadForecast));

            model.ClearLoadForecast.Execute();

            Assert.AreEqual(0, model.LoadForecast.Count);
        }
    }
}
