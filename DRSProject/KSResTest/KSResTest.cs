using CommonLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSResTest
{
    [TestFixture]
    public class KSResTest
    {
        private Generator generator1 = null;
        private Generator generator2 = null;
        private Generator generator3 = null;
        private Generator generator4 = null;
        private Generator generator5 = null;
        private Generator generator6 = null;
        private UpdateInfo update = null;
        private KSRes.Services.KSRes service = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            generator1 = new Generator();
            generator2 = new Generator();
            generator3 = new Generator();
            generator4 = new Generator();
            generator5 = new Generator();
            generator6 = new Generator();
            update = new UpdateInfo();
            service = new KSRes.Services.KSRes();

            generator1.MRID = "1";
            generator2.MRID = "2";
            generator3.MRID = "3";
            generator4.MRID = "4";
            generator5.MRID = "5";
            generator6.MRID = "6";

            generator1.ActivePower = 7;
            generator2.ActivePower = 8;
            generator3.ActivePower = 5;
            generator4.ActivePower = 3;
            generator5.ActivePower = 11;
            generator6.ActivePower = 9;

            generator1.Pmin = 2;
            generator2.Pmin = 2;
            generator3.Pmin = 2;
            generator4.Pmin = 2;
            generator5.Pmin = 2;
            generator6.Pmin = 2;

            generator1.Pmax = 20;
            generator2.Pmax = 20;
            generator3.Pmax = 20;
            generator4.Pmax = 20;
            generator5.Pmax = 20;
            generator6.Pmax = 20;

            generator1.Price = 5;
            generator2.Price = 3;
            generator3.Price = 8;
            generator4.Price = 12;
            generator5.Price = 11;
            generator6.Price = 4;

            update.Generators.Add(generator1);
            update.Generators.Add(generator2);
            update.Generators.Add(generator3);
            update.Generators.Add(generator4);
            update.Generators.Add(generator5);
            update.Generators.Add(generator6);
        }

        [TearDown]
        public void SetDataForTest()
        {
           
        }
    }
}
