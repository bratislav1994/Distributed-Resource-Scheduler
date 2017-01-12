using CommonLibrary;
using CommonLibrary.Exceptions;
using CommonLibrary.Interfaces;
using KSRes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using System.IO;
using System.Threading;
using KSRes.Data;
using KSRes.Access;
using System.Security.Cryptography;

namespace KSResTest
{
    [TestFixture]
    public class ControlerTest
    {
        private Controler controler = null;
        private ILKRes mockService = null;
        private IKSClient mockClient = null;
        private ILocalDB mockDataBase = null;
        private UpdateInfo update = null;
        private Generator generator1 = null;
        private Generator generator2 = null;
        private Generator generator3 = null;
        private Generator generator4 = null;
        private Generator generator5 = null;
        private Generator generator6 = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            mockService = Substitute.For<ILKRes>();
            mockService.Ping().Returns("OK");

            mockDataBase = Substitute.For<ILocalDB>();
            LocalDB.Instance = mockDataBase;

            controler = new Controler();

            mockClient = Substitute.For<IKSClient>();

            generator1 = new Generator();
            generator2 = new Generator();
            generator3 = new Generator();
            generator4 = new Generator();
            generator5 = new Generator();
            generator6 = new Generator();
            update = new UpdateInfo();

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
            LKResService temp = new LKResService("user1", mockService, "sessionId1");
            controler.ActiveService.Add(temp);
            controler.Update("sessionId1", update);

            update.Generators.Clear();
            update.Generators.Add(generator3);
            update.Generators.Add(generator4);
            LKResService temp1 = new LKResService("user2", mockService, "sessionId2");
            controler.ActiveService.Add(temp1);
            controler.Update("sessionId2", update);

            update.Generators.Clear();
            update.Generators.Add(generator5);
            update.Generators.Add(generator6);
            LKResService temp2 = new LKResService("user3", mockService, "sessionId3");
            controler.ActiveService.Add(temp2); ;
            controler.Update("sessionId3", update);
        }


        

        [Test]
        public void ConstructorTest()
        {
            Controler controlerTest = null;
            Assert.DoesNotThrow(() => {
                controlerTest = new Controler();
            });

            Assert.AreNotEqual(null, controlerTest.ActiveService);
            Assert.AreNotEqual(null, controlerTest.Clients);
        }

        [Test]
        [TestCase("user1", "sessionId1")]
        [TestCase("user2", "sessionId2")]
        public void GetServiceTest1(string username, string sessionId)
        {
            Assert.AreEqual(username, controler.GetService(username).Username);
            Assert.AreEqual(sessionId, controler.GetService(username).SessionID);
            Assert.AreNotEqual(null, controler.GetService(username).Client);
            Assert.AreNotEqual(null, controler.GetService(username).Generators);
            Assert.AreNotEqual(null, controler.GetService(username).Gropus);
            Assert.AreNotEqual(null, controler.GetService(username).Sites);
        }

        [Test]
        [TestCase("user9")]
        [TestCase("user20")]
        public void GetServiceTest2(string username)
        {
            Assert.AreEqual(null, controler.GetService(username));
        }

        [Test]
        public void UpdateTest1_Add()
        {
            string username = "userTest";
            string sessionId = "sessionIdTest";
            LKResService service = new LKResService(username, mockService, sessionId);
            controler.ActiveService.Add(service);

            controler.GetService(username).Generators.Add(new Generator());

            Generator genTest = new Generator();
            genTest.MRID = "99";
            genTest.GroupID = "99";

            Group groupTest = new Group();
            groupTest.MRID = "99";

            Site siteTest = new Site();
            siteTest.MRID = "99";

            update = new UpdateInfo();
            update.Generators.Add(genTest);
            update.Groups.Add(groupTest);
            update.Sites.Add(siteTest);

            controler.Update(sessionId, update);

            Assert.AreEqual(genTest.MRID, controler.GetService(username).Generators[1].MRID);
            Assert.AreEqual(genTest.GroupID, controler.GetService(username).Generators[1].GroupID);
            Assert.AreEqual(groupTest.MRID, controler.GetService(username).Gropus[0].MRID);
            Assert.AreEqual(siteTest.MRID, controler.GetService(username).Sites[0].MRID);
        }

        [Test]
        public void UpdateTest2_Add_InvalideSessionId()
        {
            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                controler.Update("invaldeSessionId", update);
            });
        }

        [Test]
        public void UpdateTest3_Add_UpdateInvalid()
        {
            Assert.Throws<InvalidDataException>(() =>
            {
                controler.Update("sessionIdTest", null);
            });
        }

        [Test]
        public void UpdateTest4_Add_GeneratorNull()
        {
            update.Generators = null;
            update.Groups = null;
            update.Sites = null;

            Assert.Throws<InvalidDataException>(() =>
            {
                controler.Update("sessionIdTest", update);
            });
        }

        [Test]
        public void UpdateTest5_Update()
        {
            string username = "userTest";
            string sessionId = "sessionIdTest";

            Generator genTest = new Generator();
            genTest.MRID = "99";
            genTest.GroupID = "98";
            genTest.ActivePower = 5;

            Group groupTest = new Group();
            groupTest.MRID = "98";

            Site siteTest = new Site();
            siteTest.MRID = "98";

            update = new UpdateInfo();
            update.UpdateType = UpdateType.UPDATE;
            update.Generators.Add(genTest);
            update.Groups.Add(groupTest);
            update.Sites.Add(siteTest);

            controler.Update(sessionId, update);
            Assert.AreEqual(genTest.MRID, controler.GetService(username).Generators[1].MRID);
            Assert.AreEqual(genTest.GroupID, controler.GetService(username).Generators[1].GroupID);
            Assert.AreEqual(groupTest.MRID, controler.GetService(username).Gropus[1].MRID);
            Assert.AreEqual(siteTest.MRID, controler.GetService(username).Sites[1].MRID);
        }

        [Test]
        public void UpdateTest6_Remove()
        {
            string username = "userTest";
            string sessionId = "sessionIdTest";

            LKResService service = controler.GetService(username);
            service.Generators.Clear();
            service.Gropus.Clear();
            service.Sites.Clear();

            controler.Update(sessionId, update);

            Assert.AreEqual(1, controler.GetService(username).Generators.Count);
            Assert.AreEqual(1, controler.GetService(username).Gropus.Count);
            Assert.AreEqual(1, controler.GetService(username).Sites.Count);

            update.UpdateType = UpdateType.REMOVE;

            controler.Update(sessionId, update);
            Assert.AreEqual(0, controler.GetService(username).Generators.Count);
            Assert.AreEqual(0, controler.GetService(username).Gropus.Count);
            Assert.AreEqual(0, controler.GetService(username).Sites.Count);
        }

        [Test]
        public void CheckIfLKServiceIsAliveTest()
        {
            ILKRes mockServiceTemp = Substitute.For<ILKRes>();
            mockServiceTemp.When(x => x.Ping()).Throw(new CommunicationException());
            LKResService service = new LKResService("newTest", mockServiceTemp, "sessionIdnewTest");
            controler.ActiveService.Add(service);

            Assert.AreEqual(4, controler.ActiveService.Count);

            Thread.Sleep(1000);

            Assert.AreEqual(3, controler.ActiveService.Count);
        }

        [Test]
        public void AddClientTest()
        {
            controler.Clients.Add(mockClient);

            Assert.AreEqual(mockClient, controler.Clients[0]);
        }

        [Test]
        public void NotifyClientsTest1()
        {
            controler.Clients.Add(mockClient);
            update = new UpdateInfo();

            Assert.DoesNotThrow(() => controler.Update("sessionId3", update));
        }

        [Test]
        public void NotifyClientsTest2_NotActiveClient()
        {
            IKSClient mockClientTemp = Substitute.For<IKSClient>();
            mockClientTemp.When(x => x.Update(update, "user3")).Throw(new CommunicationException());
            controler.Clients.Add(mockClientTemp);

            Assert.DoesNotThrow(() => controler.Update("sessionId3", update));
        }

        [Test]
        public void SendMeasurementTest()
        {
            LKResService service = new LKResService("user", mockService, "sessionId");
            controler.ActiveService.Add(service);
            controler.GetService("user").Generators.Add(new Generator() { MRID = "1", ActivePower = 6 });
            controler.GetService("user").Generators.Add(new Generator() { MRID = "3", ActivePower = 5 });
            controler.GetService("user").Generators.Add(new Generator() { MRID = "2", ActivePower = 8 });
            controler.GetService("user").Generators.Add(new Generator() { MRID = "9", ActivePower = 1 });
            controler.Clients.Add(mockClient);

            Dictionary<string, double> measurement = new Dictionary<string, double>();
            measurement.Add("1", 3);
            measurement.Add("3", 33);
            measurement.Add("2", 333);
            measurement.Add("9", 3333);

            Assert.DoesNotThrow(() => controler.SendMeasurement("user", measurement));

            Assert.AreEqual("1", controler.MultiThreadBuffer[0].MRID);
            Assert.AreEqual(3, controler.MultiThreadBuffer[0].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[0].Username);

            Assert.AreEqual("3", controler.MultiThreadBuffer[1].MRID);
            Assert.AreEqual(33, controler.MultiThreadBuffer[1].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[1].Username);

            Assert.AreEqual("2", controler.MultiThreadBuffer[2].MRID);
            Assert.AreEqual(333, controler.MultiThreadBuffer[2].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[2].Username);

            Assert.AreEqual("9", controler.MultiThreadBuffer[3].MRID);
            Assert.AreEqual(3333, controler.MultiThreadBuffer[3].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[3].Username);

            measurement["1"] = 4;
            measurement["3"] = 44;
            measurement.Remove("2");
            measurement.Remove("9");

            Assert.DoesNotThrow(() => controler.SendMeasurement("user", measurement));

            Assert.AreEqual("1", controler.MultiThreadBuffer[0].MRID);
            Assert.AreEqual(4, controler.MultiThreadBuffer[0].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[0].Username);

            Assert.AreEqual("3", controler.MultiThreadBuffer[1].MRID);
            Assert.AreEqual(44, controler.MultiThreadBuffer[1].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[1].Username);

            Assert.AreEqual("2", controler.MultiThreadBuffer[2].MRID);
            Assert.AreEqual(333, controler.MultiThreadBuffer[2].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[2].Username);

            Assert.AreEqual("9", controler.MultiThreadBuffer[3].MRID);
            Assert.AreEqual(3333, controler.MultiThreadBuffer[3].ActivePower);
            Assert.AreEqual("user", controler.MultiThreadBuffer[3].Username);
        }

        [Test]
        public void P_01()
        {
            double requiredAP = 50;
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "2");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 15);
        }

        [Test]
        public void P_02_Sp()
        {
            double requiredAP = 60;
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "2");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 14);
        }

        [Test]
        public void P_02_Bp()
        {
            double requiredAP = 60;
            List<Point> setPoints = new List<Point>();
            Generator generator2 = controler.GetService("user1").Generators.Where(x => x.MRID.Equals("2")).FirstOrDefault();
            generator2.WorkingMode = WorkingMode.LOCAL;

            List<Point> retVal = controler.P(requiredAP, true);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "2");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 14);

            generator2.WorkingMode = WorkingMode.REMOTE;
        }

        [Test]
        public void P_03()
        {
            double requiredAP = 130;
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = controler.P(requiredAP, false);

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
        public void P_04()
        {
            double requiredAP = 50;
            List<Point> setPoints = new List<Point>();
            UpdateInfo update1 = new UpdateInfo();

            update1.UpdateType = UpdateType.UPDATE;
            generator2.WorkingMode = WorkingMode.LOCAL;

            update1.Generators.Add(generator2);

            controler.Update("sessionId1", update1);

            List<Point> retVal = controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 16);
        }

        [Test]
        public void P_05()
        {
            double requiredAP = 60;
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = controler.P(requiredAP, false);

            NUnit.Framework.Assert.AreEqual(retVal[0].GeneratorID, "6");
            NUnit.Framework.Assert.AreEqual(retVal[0].Power, 20);

            NUnit.Framework.Assert.AreEqual(retVal[1].GeneratorID, "1");
            NUnit.Framework.Assert.AreEqual(retVal[1].Power, 13);
        }

        [Test]
        public void P_06()
        {
             double requiredAP = 20;
            List<Point> setPoints = new List<Point>();

            List<Point> retVal = controler.P(requiredAP, false);

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
        public void P_07()
        {
            double requiredAP = 20;
            List<Point> setPoints = new List<Point>();

            generator1.ActivePower = 5;
            generator2.ActivePower = 10;

            List<Point> retVal = controler.P(requiredAP, false);

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

        [Test]
        public void DeploySetPointTest_01()
        {
            Point setPoint = new Point();
            setPoint.GeneratorID = "test1";

            Point setPoint1 = new Point();
            setPoint1.GeneratorID = "1";

            Point setPoint2 = new Point();
            setPoint2.GeneratorID = "2";

            Point setPoint3 = new Point();
            setPoint3.GeneratorID = "test2";

            List<Point> retVal = null;
            List<Point> parameter = new List<Point>();
            parameter.Add(setPoint);
            parameter.Add(setPoint1);
            parameter.Add(setPoint2);
            parameter.Add(setPoint3);

            var foo = Substitute.For<ILKRes>();
            foo.When(x => x.SendSetPoint(Arg.Any<List<Point>>()))
                .Do(x => retVal = x.Arg<List<Point>>());

            LKResService service = new LKResService("test", foo, "sessionTest");
            service.Generators.Add(new Generator() { MRID = "test1"});
            service.Generators.Add(new Generator() { MRID = "test2" });
            controler.ActiveService.Add(service);

            controler.DeploySetPoint(parameter);

            Assert.AreEqual("test1", retVal[0].GeneratorID);
            Assert.AreEqual("test2", retVal[1].GeneratorID);
            Assert.AreEqual(2, retVal.Count);
        }

        [Test]
        public void GetProductionHistory()
        {
            DateTime time = DateTime.Now;

            ProductionHistory p = new ProductionHistory();
            p.MRID = "1";
            p.ActivePower = 5;
            p.TimeStamp = time;

            ProductionHistory p1 = new ProductionHistory();
            p1.MRID = "2";
            p1.ActivePower = 6;
            p1.TimeStamp = time;

            ProductionHistory p2 = new ProductionHistory();
            p2.MRID = "3";
            p2.ActivePower = 7;
            p2.TimeStamp = time.AddMinutes(-1);

            ProductionHistory p3 = new ProductionHistory();
            p3.MRID = "4";
            p3.ActivePower = 1;
            p3.TimeStamp = time;

            List<ProductionHistory> test = new List<ProductionHistory>();
            test.Add(p);
            test.Add(p2);
            test.Add(p3);
            test.Add(p1);

            LocalDB.Instance.ReadProductions(Arg.Any<DateTime>()).Returns(test);

            SortedDictionary<DateTime, double> retVal = controler.GetProductionHistory(5);

            DateTime condition = DateTime.Now.AddMinutes(0 - 5);

            foreach(DateTime key in retVal.Keys)
            {
                List<ProductionHistory> temp = test.Where(x => x.TimeStamp.Equals(key)).ToList();
                double sum = 0;
                foreach(ProductionHistory productionHistory in temp)
                {
                    sum += productionHistory.ActivePower;
                }

                Assert.AreEqual(sum, retVal[key]);
            }
        }

        [Test]
        public void GetAllBasePointsForUser()
        {
            List<Point> basePoints = new List<Point>();

            Point basePoint = new Point();
            basePoint.GeneratorID = "2";
            basePoint.Power = 6;

            basePoints.Add(basePoint);

            Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controler);
            List<Point> allBasePoints = (List<Point>)pObject.Invoke("GetAllBasePointsForUser", "user1", basePoints);

            Assert.AreEqual(2, allBasePoints.Count);
            Assert.AreEqual(7, allBasePoints.Where(o => o.GeneratorID.Equals("1")).FirstOrDefault().Power);
        }

        [Test]
        public void Registration_01()
        {
            LocalDB.Instance.GetService(Arg.Any<string>()).Returns(new RegisteredService() { Username = "user", Password = Encoding.ASCII.GetBytes("pass") });
            
            Assert.Throws<FaultException<IdentificationExeption>>(() => controler.Registration("user", "pass"));
        }

        [Test]
        public void Registration_02()
        {
            RegisteredService ret = null;
            LocalDB.Instance.GetService(Arg.Any<string>()).Returns(ret);

            Assert.DoesNotThrow(() => controler.Registration("user", "pass"));
        }

        [Test]
        public void Login_01_NoReg()
        {
            RegisteredService ret = null;
            LocalDB.Instance.GetService(Arg.Any<string>()).Returns(ret);

            Assert.Throws<FaultException<IdentificationExeption>>(() => controler.Login("user", "pass", mockService, "sessionId"));
        }

        [Test]
        public void Login_02_InvalidePassword()
        {
            RegisteredService ret = new RegisteredService() { Username = "user", Password = Encoding.ASCII.GetBytes("npass") };
            LocalDB.Instance.GetService(Arg.Any<string>()).Returns(ret);

            Assert.Throws<FaultException<IdentificationExeption>>(() => controler.Login("user", "pass", mockService, "sessionId"));
        }

        [Test]
        public void Login_03()
        {
            RegisteredService ret = new RegisteredService() { Username = "loginUser", Password = HashAlgorithm.Create().ComputeHash(Encoding.ASCII.GetBytes("pass")) };
            LocalDB.Instance.GetService(Arg.Any<string>()).Returns(ret);

            controler.Login("loginUser", "pass", mockService, "sessionIdLogIn");
            Assert.AreNotEqual(null, controler.ActiveService.Where(o => o.Username.Equals("loginUser")).FirstOrDefault());
        }

        [Test]
        public void Login_04()
        {
            RegisteredService ret = new RegisteredService() { Username = "loginUser", Password = HashAlgorithm.Create().ComputeHash(Encoding.ASCII.GetBytes("pass")) };
            LocalDB.Instance.GetService(Arg.Any<string>()).Returns(ret);

            Assert.Throws<FaultException<IdentificationExeption>>(() => controler.Login("loginUser", "pass", mockService, "sessionIdLogIn"));
        }

        [Test]
        public void LastValuesLC_01()
        {
            controler.LastValuesLC = new SortedDictionary<DateTime, double>();
            Assert.AreNotEqual(null, controler.LastValuesLC);
        }
    }
}
