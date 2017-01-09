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

            Group group = new Group();
            group.MRID = "g";
            group.Name = "group";
            group.SiteID = "s";

            Site site = new Site();
            site.MRID = "s";
            site.Name = "site";

            user1.Generators.Add(generator1);
            user1.Generators.Add(generator2);
            user2.Generators.Add(generator3);
            user2.Generators.Add(generator4);

            user2.Gropus.Add(group);
            user2.Sites.Add(site);

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
            Assert.AreNotEqual(null, client.Proxy);
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
            client.SetCurrentUser("user2");
            client.Proxy = mockService;

            Assert.AreEqual(2, client.Generators.Count);
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("3")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("4")).First());
        }

        [Test]
        public void GeneratorsTest()
        {
            BindingList<Generator> generators = new BindingList<Generator>();
            client.Generators = generators;

            Assert.AreNotEqual(null, client.Generators);
        }

        [Test]
        public void SetCurrentUserTest()
        {
            client.Generators.Clear();
            client.SetCurrentUser("All");

            Assert.AreEqual(4, client.Generators.Count);
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("1")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("2")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("3")).First());
            Assert.AreNotEqual(null, client.Generators.Where(x => x.MRID.Equals("4")).First());
        }

        [Test]
        public void UserNamesTest()
        {
            List<string> users = new List<string>();
            client.UserNames = users;

            Assert.AreNotEqual(null, client.UserNames);
        }

        [Test]
        public void UpdateTest_01()
        {
            Assert.Throws<ArgumentException>(() => { client.Update(null, "user1"); });
        }

        [Test]
        public void UpdateTest_02()
        {
            UpdateInfo update1 = new UpdateInfo();

            Generator generator = new Generator();
            generator.MRID = "5";
            generator.ActivePower = 3;

            update1.Generators.Add(generator);
            update1.Groups = null;
            update1.Sites = null;

            client.Update(update1, "user3");
            LKResService user3 = client.GetUser("user3");

            Assert.AreEqual(1, user3.Generators.Count);
            Assert.DoesNotThrow(() => user3.Generators.Where(x => x.MRID.Equals("5")).First());
        }

        [Test]
        public void UpdateTest_03()
        {
            UpdateInfo update1 = new UpdateInfo();

            Generator generator = new Generator();
            generator.MRID = "5";
            generator.ActivePower = 3;

            Group group = new Group();
            group.MRID = "g1";

            Site site = new Site();
            site.MRID = "s1";

            update1.Generators.Add(generator);
            update1.Groups.Add(group);
            update1.Sites.Add(site);

            LKResService user3 = client.GetUser("user3");
            user3.Generators.Clear();

            client.Update(update1, "user3");

            Assert.AreEqual(1, user3.Generators.Count);
            Assert.DoesNotThrow(() => user3.Generators.Where(x => x.MRID.Equals("5")).First());

            Assert.AreEqual(1, user3.Gropus.Count);
            Assert.DoesNotThrow(() => user3.Gropus.Where(x => x.MRID.Equals("g1")).First());

            Assert.AreEqual(1, user3.Sites.Count);
            Assert.DoesNotThrow(() => user3.Sites.Where(x => x.MRID.Equals("s1")).First());
        }

        [Test]
        public void UpdateTest_04()
        {
            UpdateInfo update1 = new UpdateInfo();
            update1.UpdateType = UpdateType.REMOVE;

            Generator generator = new Generator();
            generator.MRID = "5";

            update1.Generators.Add(generator);
            update1.Groups = null;
            update1.Sites = null;

            client.Update(update1, "user3");
            LKResService user3 = client.GetUser("user3");

            Assert.AreEqual(0, user3.Generators.Count);
            Assert.Throws<InvalidOperationException>(() => user3.Generators.Where(x => x.MRID.Equals("5")).First());
        }

        [Test]
        public void UpdateTest_05()
        {
            UpdateInfo update1 = new UpdateInfo();
            update1.UpdateType = UpdateType.REMOVE;

            Group group = new Group();
            group.MRID = "g1";

            Site site = new Site();
            site.MRID = "s1";

            update1.Groups.Add(group);
            update1.Sites.Add(site);

            LKResService user3 = client.GetUser("user3");

            client.Update(update1, "user3");

            Assert.AreEqual(0, user3.Gropus.Count);
            Assert.Throws<InvalidOperationException>(() => user3.Gropus.Where(x => x.MRID.Equals("g1")).First());

            Assert.AreEqual(0, user3.Sites.Count);
            Assert.Throws<InvalidOperationException>(() => user3.Sites.Where(x => x.MRID.Equals("s1")).First());
        }

        [Test]
        public void UpdateTest_06()
        {
            UpdateInfo update1 = null;
            Generator generator = null;
            Group group = null;
            Site site = null;
            update1 = new UpdateInfo();

            generator = new Generator();
            generator.MRID = "5";
            generator.ActivePower = 3;
            generator.GroupID = "g1";

            group = new Group();
            group.MRID = "g1";
            group.SiteID = "s1";

            site = new Site();
            site.MRID = "s1";

            update1.Generators.Add(generator);
            update1.Groups.Add(group);
            update1.Sites.Add(site);

            LKResService user3 = client.GetUser("user3");
            user3.Generators.Clear();

            client.Update(update1, "user3");

            update1 = new UpdateInfo();
            update1.UpdateType = UpdateType.UPDATE;

            generator = new Generator();
            generator.MRID = "5";
            generator.ActivePower = 99;
            generator.GroupID = "g1";

            update1.Generators.Add(generator);

            client.Update(update1, "user3");

            Assert.AreEqual(99, user3.Generators.Where(x => x.MRID.Equals("5")).First().ActivePower);
        }

        [Test]
        public void UpdateTest_07()
        {
            UpdateInfo update1 = null;
            Generator generator = null;
            Group group = null;
            Site site = null;

            update1 = new UpdateInfo();
            update1.UpdateType = UpdateType.UPDATE;

            generator = new Generator();
            generator.MRID = "5";
            generator.ActivePower = 88;
            generator.GroupID = "g2";

            group = new Group();
            group.MRID = "g2";
            group.SiteID = "s2";

            site = new Site();
            site.MRID = "s2";

            LKResService user3 = client.GetUser("user3");

            update1.Generators.Add(generator);
            update1.Groups.Add(group);
            update1.Sites.Add(site);

            client.Update(update1, "user3");

            Assert.AreEqual(88, user3.Generators.Where(x => x.MRID.Equals("5")).First().ActivePower);
            Assert.DoesNotThrow(() => user3.Gropus.Where(x => x.MRID.Equals("g2")).First());
            Assert.DoesNotThrow(() => user3.Sites.Where(x => x.MRID.Equals("s2")).First());
        }

        [Test]
        public void GetUserTest_01()
        {
            Assert.AreEqual(null, client.GetUser("user4"));
        }

        [Test]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        public void GetGeneratorFromIdTest_01(string mrID)
        {
            Assert.AreNotEqual(null, client.GetGeneratorFromId(mrID));
        }

        [Test]
        [TestCase("99")]

        public void GetGeneratorFromIdTest_02(string mrID)
        {
            Assert.AreEqual(null, client.GetGeneratorFromId(mrID));
        }

        [Test]
        public void GetGroupNameFromIdTest_01()
        {
            Assert.AreEqual("group", client.GetGroupNameFromId("g"));
        }

        [Test]
        public void GetGroupNameFromIdTest_02()
        {
            Assert.AreEqual(null, client.GetGroupNameFromId("g99"));
        }

        [Test]
        public void GetSiteNameFromIdTest_01()
        {
            Assert.AreEqual("site", client.GetSiteNameFromId("s"));
        }

        [Test]
        public void GetSiteNameFromIdTest_02()
        {
            Assert.AreEqual(null, client.GetSiteNameFromId("s99"));
        }

        [Test]
        public void GetGroupFromIdTest_01()
        {
            Assert.AreNotEqual(null, client.GetGroupFromId("g"));
        }

        [Test]
        public void GetGroupFromIdTest_02()
        {
            Assert.AreEqual(null, client.GetGroupFromId("g99"));
        }

        [Test]
        public void IssueCommand()
        {
            client.Proxy = mockService;

            Assert.DoesNotThrow(() => client.IssueCommand(7));
        }
    }
}
