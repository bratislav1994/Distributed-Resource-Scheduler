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

namespace KSResTest
{
    [TestFixture]
    public class DynamicDataBaseTest
    {
        DynamicDataBase database = null;
        ILKRes mockService = null;
        IKSClient mockClient = null;
        Generator generator = null;
        Group group = null;
        Site site = null;
        UpdateInfo update = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            mockService = Substitute.For<ILKRes>();
            mockService.Ping().Returns("OK");
            database = new DynamicDataBase();

            mockClient = Substitute.For<IKSClient>();
            
            generator = new Generator();
            generator.MRID = "0";
            generator.GroupID = "1";

            group = new Group();
            group.MRID = "1";
            group.SiteID = "2";

            site = new Site();
            site.MRID = "2";
        }

        [TearDown]
        public void SetDataForTest()
        {
            database.ActiveService.Clear();
            database.Clients.Clear();
            database.RegistrationService.Clear();

            update = new UpdateInfo();
            update.Generators.Add(generator);
            update.Groups.Add(group);
            update.Sites.Add(site);
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
            Assert.AreNotEqual(null, database.RegistrationService);
        }

        [Test]
        public void ActiveServiceTest1()
        {
            Assert.AreNotEqual(null, database.ActiveService);
        }

        [Test]
        public void ClientsTest1()
        {
            Assert.AreNotEqual(null, database.Clients);
        }

        [Test]
        [TestCase("user1")]
        [TestCase("user2")]
        public void GetServiceTest1(string username)
        {
            database.Registration("temp", "111");
            database.Login("temp", "111", mockService, "tempsessionId" + username);

            database.Registration(username, "111");
            database.Login(username, "111", mockService, "sessionId" + username);

            Assert.AreEqual(username, database.GetService(username).Username);
            Assert.AreEqual("sessionId" + username, database.GetService(username).SessionID);
            Assert.AreNotEqual(null, database.GetService(username).Client);
            Assert.AreNotEqual(null, database.GetService(username).Generators);
            Assert.AreNotEqual(null, database.GetService(username).Gropus);
            Assert.AreNotEqual(null, database.GetService(username).Sites);
        }

        [Test]
        [TestCase("user1")]
        [TestCase("user2")]
        public void GetServiceTest2(string username)
        {
            Assert.AreEqual(null, database.GetService(username));
        }

        [Test]
        [TestCase("user1", "123")]
        [TestCase("user2", "456")]
        public void RegistrationTest1(string username, string password)
        {
            database.Registration(username, password);

            Assert.AreEqual(true, database.RegistrationService.ContainsKey(username));
            Assert.AreEqual(password, database.RegistrationService[username]);
        }

        [Test]
        [TestCase("user1", "123")]
        public void RegistrationTest2_2xReg(string username, string password)
        {
            database.Registration(username, password);
            Assert.Throws<FaultException<IdentificationExeption>>(() => 
            {
                database.Registration(username, password);
            }); 
        }

        [Test]
        [TestCase("user1", "123", "sessionId")]
        public void LoginTest1(string username, string password, string sessionID)
        {
            database.Registration(username, password);

            database.Login(username, password, mockService, sessionID);
            Assert.AreEqual(username, database.ActiveService[0].Username);
        }

        [Test]
        [TestCase("user1", "123", "sessionId")]
        public void LoginTest2_BadPassword(string username, string password, string sessionID)
        {
            database.Registration(username, "incorrectPassword");

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Login(username, password, mockService, sessionID);
            });
        }

        [Test]
        [TestCase("user1", "123", "sessionId")]
        public void LoginTest3_NotRegUser(string username, string password, string sessionID)
        {
            database.Registration("notRegUser", password);

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Login(username, password, mockService, sessionID);
            });
        }

        [Test]
        [TestCase("user1", "123", "sessionId")]
        public void LoginTest3_2xLogin(string username, string password, string sessionID)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, sessionID);

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Login(username, password, mockService, sessionID);
            });
        }

        [Test]
        [TestCase("user1", "123")]
        public void UpdateTest1_Add(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");
            database.GetService(username).Generators.Add(new Generator());

            database.Update("sessionId", update);

            Assert.AreEqual(generator.MRID, database.GetService(username).Generators[1].MRID);
            Assert.AreEqual(generator.GroupID, database.GetService(username).Generators[1].GroupID);
            Assert.AreEqual(group.MRID, database.GetService(username).Gropus[0].MRID);
            Assert.AreEqual(site.MRID, database.GetService(username).Sites[0].MRID);
        }

        [Test]
        [TestCase("user1", "123")]
        public void UpdateTest2_Add_InvalideSessionId(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");

            Assert.Throws<FaultException<IdentificationExeption>>(() =>
            {
                database.Update("invaldeSessionId", update);
            });
        }

        [Test]
        [TestCase("user1", "123")]
        public void UpdateTest3_Add_UpdateInvalid(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");

            Assert.Throws<InvalidDataException>(() =>
            {
                database.Update("sessionId", null);
            });
        }

        [Test]
        [TestCase("user1", "123")]
        public void UpdateTest4_Add_GeneratorNull(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");

            update.Generators = null;

            Assert.Throws<InvalidDataException>(() =>
            {
                database.Update("sessionId", update);
            });
        }

        [Test]
        [TestCase("user1", "123")]
        public void UpdateTest5_Update(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");
            database.Update("sessionId", update);

            generator.ActivePower = 10;

            database.Update("sessionId", update);
            Assert.AreEqual(generator.MRID, database.GetService(username).Generators[0].MRID);
            Assert.AreEqual(generator.GroupID, database.GetService(username).Generators[0].GroupID);
            Assert.AreEqual(group.MRID, database.GetService(username).Gropus[0].MRID);
            Assert.AreEqual(site.MRID, database.GetService(username).Sites[0].MRID);
        }

        [Test]
        [TestCase("user1", "123")]
        public void UpdateTest6_Remove(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");
            database.Update("sessionId", update);

            update.UpdateType = UpdateType.REMOVE;

            database.Update("sessionId", update);
            Assert.AreEqual(0, database.GetService(username).Generators.Count);
            Assert.AreEqual(0, database.GetService(username).Gropus.Count);
            Assert.AreEqual(0, database.GetService(username).Sites.Count);
        }

        [Test]
        [TestCase("user1", "123")]
        public void CheckIfLKServiceIsAliveTest(string username, string password)
        {
            ILKRes mockServiceTemp = Substitute.For<ILKRes>();
            mockServiceTemp.When(x => x.Ping()).Throw(new CommunicationException());
            database.Registration(username, password);
            database.Login(username, password, mockServiceTemp, "sessionId");

            Thread.Sleep(1000);

            Assert.AreEqual(0, database.ActiveService.Count);
        }

        [Test]
        [TestCase("user1", "123")]
        public void CheckIfLKServiceIsAliveTest2(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");

            Thread.Sleep(1000);

            Assert.AreEqual(1, database.ActiveService.Count);
        }

        [Test]
        public void AddClientTest()
        {
            database.AddClient(mockClient);

            Assert.AreEqual(mockClient, database.Clients[0]);
        }

        [Test]
        [TestCase("user1", "123")]
        public void NotifyClientsTest1(string username, string password)
        {
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");
            database.AddClient(mockClient);

            Assert.DoesNotThrow(() =>database.Update("sessionId", update));
        }

        [Test]
        [TestCase("user1", "123")]
        public void NotifyClientsTest2_NotActiveClient(string username, string password)
        {
            IKSClient mockClientTemp = Substitute.For<IKSClient>();
            mockClientTemp.When(x => x.Update(update, username)).Throw(new CommunicationException());
            database.Registration(username, password);
            database.Login(username, password, mockService, "sessionId");
            database.AddClient(mockClientTemp);

            Assert.DoesNotThrow(() => database.Update("sessionId", update));
        }
    }
}
