using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonLibrary;
using CommonLibrary.Interfaces;
using CommonLibrary.Exceptions;
using LKRes.Services;
using System.ServiceModel;
using NSubstitute;
using KSRes;
using System.Threading;
using LKRes.Access;
using LKRes.Data;

namespace LKResTest.ServicesTest
{
    [TestFixture]
    public class LKForClientServiceTest
    {
        private LKForClientService lkResTest = null;
        private IDataBase mockDataBase = null;
        private IActivePowerManagement mockProxy = null;
        private IKSRes mockKsRes = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.lkResTest = new LKForClientService();
            this.lkResTest.Updateinfo = new UpdateInfo();
            this.lkResTest.Proxy = Substitute.For<IActivePowerManagement>();
            this.lkResTest.KSResProxy = Substitute.For<IKSRes>();
            

            this.mockDataBase = Substitute.For<IDataBase>();
            DataBase.Instance = mockDataBase;
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new LKForClientService());
        }

        [Test]
        public void PingTest()
        {
            lkResTest.Ping();

            Assert.AreNotEqual(string.Empty, lkResTest.Ping());
        }

        [Test]
        public void SetpointTest()
        {
            List<Point> setpoints = new List<Point>();
            Generator generator = new Generator()
            {
                MRID = "1"
            };

            lkResTest.Updateinfo.Generators.Add(generator);

            Point setpoint = new Point();
            setpoint.GeneratorID = generator.MRID;
            setpoint.Power = 10;
            setpoints.Add(setpoint);

            lkResTest.SendSetPoint(setpoints);

            Assert.AreNotEqual(null, setpoints);
            Assert.AreNotEqual(null, generator);
            Assert.AreNotEqual(null, setpoint);
        }

        
        [Test]
        public void UpdateAddTest01()
        {
            IKSRes mockKSRes = Substitute.For<IKSRes>();

            Assert.Throws<ArgumentNullException>(() => lkResTest.Update(null));

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.ADD;

            Generator generator = new Generator();
            info.Generators.Add(generator);

            Group group = new Group();
            info.Groups.Add(group);

            Site site = new Site();
            info.Sites.Add(site);

            

            mockKSRes.Update(info);
            lkResTest.KSResProxy = mockKSRes;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateAddTest02()
        {
            IKSRes mockKSRes = Substitute.For<IKSRes>();
            Assert.Throws<ArgumentNullException>(() => lkResTest.Update(null));

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.ADD;

            Generator generator = new Generator();
            info.Generators.Add(generator);

            Group group = new Group();
            info.Groups.Add(group);

            info.Sites = null;

            mockKSRes.Update(info);
            lkResTest.KSResProxy = mockKSRes;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateAddTest03()
        {
            IKSRes mockKSRes = Substitute.For<IKSRes>();
            Assert.Throws<ArgumentNullException>(() => lkResTest.Update(null));

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.ADD;

            Generator generator = new Generator();
            info.Generators.Add(generator);

            info.Groups = null;
            info.Sites = null;

            mockKSRes.Update(info);
            lkResTest.KSResProxy = mockKSRes;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateRemoveTest01()
        {
            //IKSRes mockKSRes = Substitute.For<IKSRes>();
            //IDataBase mockDB = Substitute.For<IDataBase>();

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.REMOVE;

            Generator generator = new Generator()
            {
                MRID = "g1"
            };

            lkResTest.Updateinfo = new UpdateInfo();

            lkResTest.Updateinfo.Generators.Add(generator);
            DataBase.Instance.AddGenerator(new GeneratorEntity()
            {
                Gen = generator
            });
            info.Generators.Add(generator);

            Group group = new Group()
            {
                MRID = "gg1"
            };

            lkResTest.Updateinfo.Groups.Add(group);
            DataBase.Instance.AddGroup(new GroupEntity()
            {
                GEntity = group
            });
            info.Groups.Add(group);

            Site site = new Site()
            {
                MRID = "s1"
            };

            lkResTest.Updateinfo.Sites.Add(site);
            DataBase.Instance.AddSite(new SiteEntity()
            {
                SEntity = site
            });
            info.Sites.Add(site);

            //DataBase.Instance = mockDataBase;
            this.lkResTest.KSResProxy.Update(info);
            //lkResTest.KSResProxy = mockKsRes;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateRemoveTest02()
        {
            //IKSRes mockKSRes = Substitute.For<IKSRes>();
            this.lkResTest.Updateinfo = new UpdateInfo();
            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.REMOVE;

            Generator generator = new Generator()
            {
                MRID = "g1"
            };
            info.Generators.Add(generator);

            info.Groups = null;
            info.Sites = null;

            this.lkResTest.KSResProxy.Update(info);
            //lkResTest.KSResProxy = mockKSRes;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateTest01()
        {
            IKSRes mockKSRes = Substitute.For<IKSRes>();

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.UPDATE;

            Generator generator1 = new Generator()
            {
                MRID = "g1",
                Name = "gen",
                ActivePower = 10.25,
                GeneratorType = GeneratorType.SOLAR,
                HasMeasurment = false
            };
            this.lkResTest.Updateinfo = new UpdateInfo();

            lkResTest.Updateinfo.Generators.Add(generator1);
//
            Generator generator2 = new Generator()
            {
                MRID = "g1",
                Name = "gen2",
                ActivePower = 20,
                GeneratorType = GeneratorType.SOLAR,
                HasMeasurment = true
            };
            info.Generators.Add(generator2);

            Group group = new Group()
            {
                MRID = "gg1"
            };
            info.Groups.Add(group);

            Site site = new Site()
            {
                MRID = "s1"
            };
            info.Sites.Add(site);

            mockKSRes.Update(info);
            lkResTest.KSResProxy = mockKSRes;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateTest02()
        {
            IKSRes mockKSRes = Substitute.For<IKSRes>();

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.UPDATE;
            Generator generator1 = new Generator()
            {
                MRID = "g1",
                Name = "gen",
                ActivePower = 10.25,
                GeneratorType = GeneratorType.SOLAR,
                HasMeasurment = false
            };

            this.lkResTest.Updateinfo = new UpdateInfo();
            lkResTest.Updateinfo.Generators.Add(generator1);
//
            Generator generator2 = new Generator()
            {
                MRID = "g2",
                Name = "gen2",
                ActivePower = 20,
                GeneratorType = GeneratorType.SOLAR,
                HasMeasurment = true
            };
            info.Generators.Add(generator2);

            info.Groups = null;
            info.Sites = null;

            mockKSRes.Update(info);
            lkResTest.KSResProxy = mockKSRes;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateTest03()
        {
            IKSRes mockKSRes = Substitute.For<IKSRes>();

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = 0;

            mockKSRes.Update(info);
            lkResTest.KSResProxy = mockKSRes;

            lkResTest.Update(info);
        }

        //[Test]
        //public void UpdateTest04()
        //{
        //    IKSRes mockKSRes = Substitute.For<IKSRes>();

        //    UpdateInfo info = new UpdateInfo();
        //    info.UpdateType = UpdateType.UPDATE;

        //    Generator generator1 = new Generator()
        //    {
        //        MRID = "1",
        //        Name = "gen",
        //        ActivePower = 10.25,
        //        GeneratorType = GeneratorType.SOLAR,
        //        WorkingMode = WorkingMode.REMOTE,
        //        HasMeasurment = false
        //    };
        //    lkResTest.updateInfo.Generators.Add(generator1);

        //    Generator generator2 = new Generator()
        //    {
        //        MRID = "1",
        //        Name = "gen2",
        //        ActivePower = 20,
        //        GeneratorType = GeneratorType.SOLAR,
        //        WorkingMode = WorkingMode.LOCAL,
        //        HasMeasurment = true
        //    };
        //    info.Generators.Add(generator2);

        //    Group group = new Group()
        //    {
        //        MRID = "gg1"
        //    };
        //    info.Groups.Add(group);

        //    Site site = new Site()
        //    {
        //        MRID = "s1"
        //    };
        //    info.Sites.Add(site);

        //    mockKSRes.Update(info);
        //    lkResTest.KSResProxy = mockKSRes;

        //    lkResTest.Update(info);
        //}

        [Test]
        public void RegistrationTest01()
        {
            IKSRes mockRes = Substitute.For<IKSRes>();
            mockRes.Registration("test", "test");

            lkResTest.KSResProxy = mockRes;
            Assert.AreEqual(mockRes, lkResTest.KSResProxy);

            lkResTest.Registration("test", "test");
        }

        [Test]
        public void LoginTest()
        {
            IKSRes mockRes = Substitute.For<IKSRes>();
            mockRes.Registration("test", "test");
            mockRes.Login("test", "test"); 

            lkResTest.KSResProxy = mockRes;
            Assert.AreEqual(mockRes, lkResTest.KSResProxy);

            lkResTest.Registration("test", "test");
            lkResTest.Login("test", "test");
        }

        [Test]
        public void ClientTest()
        {
            ILKClient mockClient = Substitute.For<ILKClient>();
            lkResTest.Client = mockClient;
            Assert.AreEqual(mockClient, lkResTest.Client);
        }

        //[Test]
        //public void ActivePowerTest()
        //{
        //    Group group = new Group()
        //    {
        //        MRID = "1"
        //    };
        //    Generator gen = new Generator()
        //    {
        //        MRID = "2",
        //        GroupID = group.MRID,
        //        HasMeasurment = true,
        //        SetPoint = -1
                    
        //    };

        //    lkResTest.updateInfo.Generators.Add(gen);
        //    lkResTest.updateInfo.Groups.Add(group);
        //    Thread thread = new Thread(() => lkResTest.ChangeActivePower());
        //    thread.Start();
        //}
    }
}
