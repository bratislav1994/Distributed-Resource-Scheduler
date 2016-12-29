using CommonLibrary;
using KSRESClient;
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

        Client client = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            client = new Client();
        }

        [Test]
        public void GeneratorsPropTest()
        {
            Generator generator = new Generator();
            generator.MRID = "123";

            BindingList<Generator> generators = new BindingList<Generator>();
            generators.Add(generator);

            client.Generators = generators;

            Assert.AreNotEqual(null, client.Generators);
            Assert.AreEqual(generator.MRID, client.Generators[0].MRID);
        }

        [Test]
        public void UserNamesPropTest()
        {
            String user = "user";

            List<string> userNames = new List<string>();
            userNames.Add(user);

            client.UserNames = userNames;

            Assert.AreNotEqual(null, client.UserNames);
            Assert.AreEqual(user, client.UserNames[0]);
        }

        public void UpdateAddTest()
        {
            UpdateInfo update = new UpdateInfo();
            update.Generators = new List<Generator>();
            update.Groups = new List<Group>();
            update.Sites = new List<Site>();
        }
    }
}
