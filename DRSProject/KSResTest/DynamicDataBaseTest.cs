using KSRes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSResTest
{
    [TestFixture]
    public class DynamicDataBaseTest
    {
        DynamicDataBase database = null;
        [OneTimeSetUp]
        public void SetupTest()
        {
            database = new DynamicDataBase();
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
    }
}
