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

namespace KSResTest
{
    [TestFixture]
    public class DynamicDataBaseTest
    {
        DynamicDataBase database = null;
        [OneTimeSetUp]
        public void SetupTest()
        {
            
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => {
                database = new DynamicDataBase();
                } );

            Assert.AreNotEqual(null, database.ActiveService);
            Assert.AreNotEqual(null, database.RegistrationService);
            Assert.AreNotEqual(null, database.Clients);
        }

        [Test]
        public void RegistrationServiceTest1()
        {
            database = new DynamicDataBase();
            Assert.AreNotEqual(null, database.RegistrationService);
        }

        [Test]
        public void ActiveServiceTest1()
        {
            database = new DynamicDataBase();
            Assert.AreNotEqual(null, database.ActiveService);
        }

        [Test]
        public void ClientsTest1()
        {
            database = new DynamicDataBase();
            Assert.AreNotEqual(null, database.Clients);
        }

        [Test]
        [TestCase("test1")]
        [TestCase("test2")]
        public void GetServiceTest1(string username)
        {
            database = new DynamicDataBase();

            database.Registration(username, "111");
            database.Login(username, "111", null, "123" + username);

            Assert.AreEqual(username, database.GetService(username).Username);
            Assert.AreEqual("123" + username, database.GetService(username).SessionID);
            Assert.AreEqual(null, database.GetService(username).Client);
            Assert.AreNotEqual(null, database.GetService(username).Generators);
            Assert.AreNotEqual(null, database.GetService(username).Gropus);
            Assert.AreNotEqual(null, database.GetService(username).Sites);
            
        }

        [Test]
        [TestCase("test3")]
        [TestCase("test4")]
        public void GetServiceTest2(string username)
        {
            database = new DynamicDataBase();
            Assert.AreEqual(null, database.GetService(username));
        }

        [Test]
        [TestCase("test3", "123")]
        [TestCase("test4", "566")]
        public void RegistrationTest1(string username, string password)
        {
            database = new DynamicDataBase();
            database.Registration(username, password);

            Assert.AreEqual(true, database.RegistrationService.ContainsKey(username));
            Assert.AreEqual(password, database.RegistrationService[username]);
        }

        [Test]
        [TestCase("test4", "566")]
        public void RegistrationTest2(string username, string password)
        {
            database = new DynamicDataBase();
            database.Registration(username, password);
            Assert.Throws<FaultException<IdentificationExeption>>(() => 
            {
                database.Registration(username, password);
            }); 
        }

        [Test]
        [TestCase("test4", "566", null, "123")]
        public void LoginTest1(string username, string password, ILKRes channel, string sessionID)
        {
            database = new DynamicDataBase();
            database.Registration(username, password);

            database.Login(username, password, channel, sessionID);
            Assert.AreEqual(username, database.ActiveService[0].Username);
        }

        [Test]
        [TestCase("test4", "999", null, "123")]
        public void LoginTest2_BadPassword(string username, string password, ILKRes channel, string sessionID)
        {
            database = new DynamicDataBase();
            database.Registration(username, "-1");

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Login(username, password, channel, sessionID);
            });
        }

        [Test]
        [TestCase("test8", "999", null, "123")]
        public void LoginTest3_NotRegUser(string username, string password, ILKRes channel, string sessionID)
        {
            database = new DynamicDataBase();
            database.Registration("test4", password);

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Login(username, password, channel, sessionID);
            });
        }

        [Test]
        [TestCase("test8", "999", null, "123")]
        public void LoginTest3_2xLogin(string username, string password, ILKRes channel, string sessionID)
        {
            database = new DynamicDataBase();
            database.Registration(username, password);
            database.Login(username, password, channel, sessionID);

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Login(username, password, channel, sessionID);
            });
        }

        [Test]
        [TestCase("test4", "566")]
        public void UpdateTest1(string username, string password)
        {
            database = new DynamicDataBase();
            database.Registration(username, password);

            ILKRes service = Substitute.For<ILKRes>();
            service.Ping().Returns("OK");

            database.Login(username, password, service, "111");

            Generator generator = new Generator();
            generator.MRID = "0";
            generator.GroupID = "1";

            Group group = new Group();
            group.MRID = "1";
            group.SiteID = "2";

            Site site = new Site();
            site.MRID = "2";

            UpdateInfo up = new UpdateInfo();
            up.Generators.Add(generator);
            up.Groups.Add(group);
            up.Sites.Add(site);

            database.Update("111", up);
        }

        [Test]
        [TestCase("test4", "566")]
        public void UpdateTest2_InvalideSessionId(string username, string password)
        {
            database = new DynamicDataBase();
            database.Registration(username, password);
            database.Login(username, password, null, "111");

            Generator generator = new Generator();
            generator.MRID = "0";
            generator.GroupID = "1";

            Group group = new Group();
            group.MRID = "1";
            group.SiteID = "2";

            Site site = new Site();
            site.MRID = "2";

            UpdateInfo up = new UpdateInfo();
            up.Generators.Add(generator);
            up.Groups.Add(group);
            up.Sites.Add(site);

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Update("125", up);
            });
        }
    }
}
