using KSRESClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRESClientTest
{
    [TestFixture]
    public class KSRESClientViewModelTest
    {
        KSRESClientViewModel model;

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
            String NeededPower = "test";
            model.NeededPower = NeededPower;

            Assert.AreEqual(NeededPower, model.NeededPower);
            Assert.AreNotEqual(null, model.NeededPower);
        }

        [Test]
        public void SelectedItemPropTest()
        {
            object SelectedItem = new object();
            model.SelectedItem = SelectedItem;

            Assert.AreEqual(SelectedItem, model.SelectedItem);
            Assert.AreNotEqual(null, model.SelectedItem);
        }

        [Test]
        public void GenMRIDPropTest()
        {
            string GenMRID = "test";
            model.GenMRID = GenMRID;

            Assert.AreEqual(GenMRID, model.GenMRID);
            Assert.AreNotEqual(null, model.GenMRID);
        }

        [Test]
        public void GenNamePropTest()
        {
            string GenName = "test";
            model.GenName = GenName;

            Assert.AreEqual(GenName, model.GenName);
            Assert.AreNotEqual(null, model.GenName);
        }

        [Test]
        public void GenWorkingModePropTest()
        {
            string GenWorkingMode = "test";
            model.GenWorkingMode = GenWorkingMode;

            Assert.AreEqual(GenWorkingMode, model.GenWorkingMode);
            Assert.AreNotEqual(null, model.GenWorkingMode);
        }

        [Test]
        public void GenTypePropTest()
        {
            string GenType = "test";
            model.GenType = GenType;

            Assert.AreEqual(GenType, model.GenType);
            Assert.AreNotEqual(null, model.GenType);
        }

        [Test]
        public void GenSPPropTest()
        {
            string GenSP = "test";
            model.GenSP = GenSP;

            Assert.AreEqual(GenSP, model.GenSP);
            Assert.AreNotEqual(null, model.GenSP);
        }

        [Test]
        public void GenPricePropTest()
        {
            string GenPrice = "test";
            model.GenPrice = GenPrice;

            Assert.AreEqual(GenPrice, model.GenPrice);
            Assert.AreNotEqual(null, model.GenPrice);
        }

        [Test]
        public void GenAPPropTest()
        {
            string GenAP = "test";
            model.GenAP = GenAP;

            Assert.AreEqual(GenAP, model.GenAP);
            Assert.AreNotEqual(null, model.GenAP);
        }

        [Test]
        public void GenBPPropTest()
        {
            string GenBP = "test";
            model.GenBP = GenBP;

            Assert.AreEqual(GenBP, model.GenBP);
            Assert.AreNotEqual(null, model.GenBP);
        }

        [Test]
        public void GenHasMeasPropTest()
        {
            string GenHasMeas = "test";
            model.GenHasMeas = GenHasMeas;

            Assert.AreEqual(GenHasMeas, model.GenHasMeas);
            Assert.AreNotEqual(null, model.GenHasMeas);
        }

        [Test]
        public void GenPMaxPropTest()
        {
            string GenPMax = "test";
            model.GenPMax = GenPMax;

            Assert.AreEqual(GenPMax, model.GenPMax);
            Assert.AreNotEqual(null, model.GenPMax);
        }

        [Test]
        public void GenPMinPropTest()
        {
            string GenPMin = "test";
            model.GenPMin = GenPMin;

            Assert.AreEqual(GenPMin, model.GenPMin);
            Assert.AreNotEqual(null, model.GenPMin);
        }

        [Test]
        public void GenSitePropTest()
        {
            string GenSite = "test";
            model.GenSite = GenSite;

            Assert.AreEqual(GenSite, model.GenSite);
            Assert.AreNotEqual(null, model.GenSite);
        }

        [Test]
        public void GenGroupPropTest()
        {
            string GenGroup = "test";
            model.GenGroup = GenGroup;

            Assert.AreEqual(GenGroup, model.GenGroup);
            Assert.AreNotEqual(null, model.GenGroup);
        }
    }
}
