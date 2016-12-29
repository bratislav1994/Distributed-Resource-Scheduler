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

        
    }
}
