using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonLibrary;
using CommonLibrary.Interfaces;
using LKRes.Services;
using System.ServiceModel;
using NSubstitute;

namespace LKResTest.ServicesTest
{
    [TestFixture]
    public class LKForClientServiceTest
    {
        private LKForClientService lkResTest = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            this.lkResTest = new LKForClientService();
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
            List<SetPoint> setpoints = new List<SetPoint>();
            Generator generator = new Generator()
            {
                MRID = "1"
            };

            lkResTest.updateInfo.Generators.Add(generator);

            SetPoint setpoint = new SetPoint();
            setpoint.GeneratorID = generator.MRID;
            setpoint.Setpoint = 10;
            setpoints.Add(setpoint);

            lkResTest.SendSetPoint(setpoints);

            Assert.AreNotEqual(null, setpoints);
            Assert.AreNotEqual(null, generator);
            Assert.AreNotEqual(null, setpoint);
        }

        [Test]
        public void UpdateAddTest01()
        {
            Assert.Throws<ArgumentNullException>(() => lkResTest.Update(null));

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.ADD;

            Generator generator = new Generator();
            info.Generators.Add(generator);

            Group group = new Group();
            info.Groups.Add(group);

            Site site = new Site();
            info.Sites.Add(site);

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateAddTest02()
        {
            Assert.Throws<ArgumentNullException>(() => lkResTest.Update(null));

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.ADD;

            Generator generator = new Generator();
            info.Generators.Add(generator);

            Group group = new Group();
            info.Groups.Add(group);

            info.Sites = null;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateAddTest03()
        {
            Assert.Throws<ArgumentNullException>(() => lkResTest.Update(null));

            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.ADD;

            Generator generator = new Generator();
            info.Generators.Add(generator);

            info.Groups = null;
            info.Sites = null;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateRemoveTest01()
        {
            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.REMOVE;

            Generator generator = new Generator()
            {
                MRID = "g1"
            };
            lkResTest.updateInfo.Generators.Add(generator);
            info.Generators.Add(generator);

            Group group = new Group()
            {
                MRID = "gg1"
            };
            lkResTest.updateInfo.Groups.Add(group);
            info.Groups.Add(group);

            Site site = new Site()
            {
                MRID = "s1"
            };
            lkResTest.updateInfo.Sites.Add(site);
            info.Sites.Add(site);

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateRemoveTest02()
        {
            UpdateInfo info = new UpdateInfo();
            info.UpdateType = UpdateType.REMOVE;

            Generator generator = new Generator()
            {
                MRID = "g1"
            };
            info.Generators.Add(generator);

            info.Groups = null;
            info.Sites = null;

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateTest01()
        {
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
            lkResTest.updateInfo.Generators.Add(generator1);

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

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateTest02()
        {
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
            lkResTest.updateInfo.Generators.Add(generator1);

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

            lkResTest.Update(info);
        }

        [Test]
        public void UpdateTest03()
        {
            UpdateInfo info = new UpdateInfo();
            info.UpdateType = 0;

            lkResTest.Update(info);
        }

        [Test]
        public void RegistrationTest()
        {
            IKSRes mockRes = Substitute.For<IKSRes>();
            mockRes.Registration("test", "test");

            lkResTest.KSResProxy = mockRes;
            lkResTest.Registration("test", "test");
        }
    }
}
