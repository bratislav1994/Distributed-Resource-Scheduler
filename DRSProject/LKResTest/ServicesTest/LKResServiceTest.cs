using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LKRes.Services;

namespace LKResTest.ServicesTest
{
    [TestFixture]
    public class LKResServiceTest
    {
        private LKResService service = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            service = new LKResService();
        }

        [Test]
        public void PingTest()
        {
            service.Ping();
        }
    }
}
