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
    public class ClientTest
    {

        private Client client = null;
        private List<LKResService> allUsers = null;
        private IKSForClient mockService = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            client = new Client();
            LKResService user1 = new LKResService("user1", null, "sessionID");
            LKResService user2 = new LKResService("user2", null, "sessionID2");

            Generator generator1 = new Generator();
            generator1.MRID = "1";
            generator1.ActivePower = 1;

            Generator generator2 = new Generator();
            generator2.MRID = "2";
            generator2.ActivePower = 1;

            Generator generator3 = new Generator();
            generator3.MRID = "3";
            generator3.ActivePower = 1;

            Generator generator4 = new Generator();
            generator4.MRID = "4";
            generator4.ActivePower = 1;

            user1.Generators.Add(generator1);
            user1.Generators.Add(generator2);
            user2.Generators.Add(generator3);
            user2.Generators.Add(generator4);

            allUsers = new List<LKResService>();
            allUsers.Add(user1);
            allUsers.Add(user2);

            mockService = Substitute.For<IKSForClient>();
            mockService.GetAllSystem().Returns(allUsers);
            client.Proxy = mockService;
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => { client = new Client(); });
            Assert.AreNotEqual(null, client.Generators);
        }

        [Test]
        public void GetSetProxyTest()
        {
            client.Proxy = mockService;
            NUnit.Framework.Assert.AreNotEqual(null, client.Proxy);
        }

        [Test]
        public void FillListForShowingTest_01()
        {
            client.Generators.Clear();
            //Kada se setuje proxi poziva se metoda getAllUser koja poziva metodu FillListForShowing koja puni listu za prikaz
            client.Proxy = mockService;

            Assert.AreEqual(4, client.Generators.Count);
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("1")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("2")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("3")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("4")).First());
        }

        [Test]
        public void FillListForShowingTest_02()
        {
            client.Generators.Clear();
            client.SetCurrentUser("user1");
            client.Proxy = mockService;

            Assert.AreEqual(2, client.Generators.Count);
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("1")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("2")).First());
        }
    }
}
