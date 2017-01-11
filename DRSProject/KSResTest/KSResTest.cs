using CommonLibrary;
using CommonLibrary.Interfaces;
using KSRes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSResTest
{
    [TestFixture]
    public class KSResTest
    {
        private Generator generator1 = null;
        private Generator generator2 = null;
        private Generator generator3 = null;
        private Generator generator4 = null;
        private Generator generator5 = null;
        private Generator generator6 = null;
        private UpdateInfo update = null;
        private KSRes.Services.KSRes service = null;
        private ILKRes mockService = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            generator1 = new Generator();
            generator2 = new Generator();
            generator3 = new Generator();
            generator4 = new Generator();
            generator5 = new Generator();
            generator6 = new Generator();
            update = new UpdateInfo();
            service = new KSRes.Services.KSRes();

            mockService = Substitute.For<ILKRes>();
            mockService.Ping().Returns("OK");
            mockService.SendSetPoint(new List<Point>());
           
            generator1.MRID = "1";
            generator2.MRID = "2";
            generator3.MRID = "3";
            generator4.MRID = "4";
            generator5.MRID = "5";
            generator6.MRID = "6";

            //sum active power = 43
            generator1.ActivePower = 7;
            generator2.ActivePower = 8;
            generator3.ActivePower = 5;
            generator4.ActivePower = 3;
            generator5.ActivePower = 11;
            generator6.ActivePower = 9;

            generator1.Pmin = 2;
            generator2.Pmin = 2;
            generator3.Pmin = 2;
            generator4.Pmin = 2;
            generator5.Pmin = 2;
            generator6.Pmin = 2;

            generator1.Pmax = 20;
            generator2.Pmax = 20;
            generator3.Pmax = 20;
            generator4.Pmax = 20;
            generator5.Pmax = 20;
            generator6.Pmax = 20;

            generator1.Price = 5;
            generator2.Price = 3;
            generator3.Price = 8;
            generator4.Price = 12;
            generator5.Price = 11;
            generator6.Price = 4;

            generator1.WorkingMode = WorkingMode.REMOTE;
            generator2.WorkingMode = WorkingMode.REMOTE;
            generator3.WorkingMode = WorkingMode.REMOTE;
            generator4.WorkingMode = WorkingMode.REMOTE;
            generator5.WorkingMode = WorkingMode.REMOTE;
            generator6.WorkingMode = WorkingMode.REMOTE;

            update.Generators.Add(generator1);
            update.Generators.Add(generator2);         
            LKResService temp = new LKResService("user1", mockService, "sessionId");
            KSRes.Services.KSRes.Controler.ActiveService.Add(temp);
            KSRes.Services.KSRes.Controler.Update("sessionId", update);

            update.Generators.Clear();
            update.Generators.Add(generator3);
            update.Generators.Add(generator4);
            LKResService temp1 = new LKResService("user12", mockService, "sessionId2");
            KSRes.Services.KSRes.Controler.ActiveService.Add(temp1);
            KSRes.Services.KSRes.Controler.Update("sessionId2", update);

            update.Generators.Clear();
            update.Generators.Add(generator5);
            update.Generators.Add(generator6);
            LKResService temp2 = new LKResService("user3", mockService, "sessionId3");
            KSRes.Services.KSRes.Controler.ActiveService.Add(temp2); ;
            KSRes.Services.KSRes.Controler.Update("sessionId3", update);
        }

        [Test]
        [TestCase(50)]
        public void IssueCommand_01(double requiredAP)
        {
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = KSRes.Services.KSRes.Controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "2");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 15);
        }

        [Test]
        [TestCase(60)]
        public void IssueCommand_02(double requiredAP)
        {
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = KSRes.Services.KSRes.Controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "2");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 14);
        }

        [Test]
        [TestCase(130)]
        public void IssueCommand_03(double requiredAP)
        {
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = KSRes.Services.KSRes.Controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "2");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[2].GeneratorID, "1");
            NUnit.Framework.Assert.AreEqual(retVal[2].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[3].GeneratorID, "3");
            NUnit.Framework.Assert.AreEqual(retVal[3].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[4].GeneratorID, "5");
            NUnit.Framework.Assert.AreEqual(retVal[4].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[5].GeneratorID, "4");
            NUnit.Framework.Assert.AreEqual(retVal[5].Power, 20);
        }

        [Test]
        [TestCase(50)]
        public void IssueCommand_04(double requiredAP)
        {
            List<Point> setPoints = new List<Point>();
            UpdateInfo update1 = new UpdateInfo();

            update1.UpdateType = UpdateType.UPDATE;
            generator2.WorkingMode = WorkingMode.LOCAL;

            update1.Generators.Add(generator2);

            KSRes.Services.KSRes.Controler.Update("sessionId", update1);

            List<Point> retVal = KSRes.Services.KSRes.Controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 16);
        }

        [Test]
        [TestCase(60)]
        public void IssueCommand_05(double requiredAP)
        {
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = KSRes.Services.KSRes.Controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "1");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 13);
        }

        [Test]
        [TestCase(20)]
        public void IssueCommand_06(double requiredAP)
        {
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = KSRes.Services.KSRes.Controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "4");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 0);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "5");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 0);

            NUnit.Framework.Assert.AreEqual(retVal[2].GeneratorID, "3");
            NUnit.Framework.Assert.AreEqual(retVal[2].Power, 0);

            NUnit.Framework.Assert.AreEqual(retVal[3].GeneratorID, "1");
            NUnit.Framework.Assert.AreEqual(retVal[3].Power, 3);
        }

        [Test]
        [TestCase(20)]
        public void IssueCommand_07(double requiredAP)
        {
            List<Point> setPoints = new List<Point>();

            generator1.ActivePower = 5;
            generator2.ActivePower = 10;

            List<Point> retVal = KSRes.Services.KSRes.Controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "4");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 0);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "5");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 0);

            NUnit.Framework.Assert.AreEqual(retVal[2].GeneratorID, "3");
            NUnit.Framework.Assert.AreEqual(retVal[2].Power, 0);

            NUnit.Framework.Assert.AreEqual(retVal[3].GeneratorID, "1");
            NUnit.Framework.Assert.AreEqual(retVal[3].Power, 2);

            NUnit.Framework.Assert.AreEqual(retVal[4].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[4].Power, 8);
        }
    }
}
