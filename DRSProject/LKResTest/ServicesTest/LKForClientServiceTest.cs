using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonLibrary;
using LKRes.Services;
using System.ServiceModel;

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
        public void ChangeActivePowerTest()
        {
            LKForClientService.updateInfo = new UpdateInfo()
            {
                Generators = new List<Generator>()
                {
                   new Generator()
                   {

                      MRID = "generator 1",
                      GroupID = "group 1",
                      WorkingMode = WorkingMode.LOCAL,
                      HasMeasurment = true
                   }
                },
                Groups = new List<Group>()
                 {
                     new Group()
                     {
                         MRID = "group 1"
                     }
                 },
                Sites = new List<Site>()
                {

                }
            };

            lkResTest.ChangeActivePower();
        }
    }
}
