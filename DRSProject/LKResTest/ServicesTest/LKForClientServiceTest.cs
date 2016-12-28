using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonLibrary;
using LKRes.Services;

namespace LKResTest.ServicesTest
{
    [TestFixture]
    public class LKForClientServiceTest
    {
        private LKForClientService lkResTest;

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
        public void GetMySystemTest()
        {
            Assert.DoesNotThrow(() => { lkResTest.GetMySystem(); });
        }

        [Test]
        public void LoginTest(string usernameTest, string passwordTest)
        {

        }

        [Test]
        public void RegistrationTest(string usernameTest, string passwordTest)
        {

        }

        [Test]
        public void UpdateTest(UpdateInfo updateTest)
        {

        }

        [Test]
        public void AddTest(UpdateInfo updateTest)
        {

        }

        [Test]
        public void RemoveTest(UpdateInfo updateTest)
        {

        }

        [Test]
        public void UpdateDataTest(UpdateInfo updateTest)
        {

        }
    }
}
