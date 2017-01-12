using CommonLibrary.Interfaces;
using LoadForecast;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadForecastTest
{
    [TestFixture]
    public class LoadForecastServiceTest
    {
        private ILoadForecast service = null;

        [OneTimeSetUp]
        public void SetupTest()
        {
            service = new LoadForecastService();
        }

        [Test]
        public void LoadForecastTest()
        {
            List<KeyValuePair<DateTime, double>> consumptions = new List<KeyValuePair<DateTime, double>>();
            DateTime time = DateTime.Now;
            consumptions.Add(new KeyValuePair<DateTime, double>(time, 10));
            consumptions.Add(new KeyValuePair<DateTime, double>(time.AddMinutes(10), 6));

            SortedDictionary<DateTime, double> retVal = service.LoadForecast(consumptions);
            List<KeyValuePair<DateTime, double>> temp = retVal.ToList();

            for(int i = 0; i < retVal.Count - 2; i++)
            {
                double k = (temp[i + 1].Value - temp[i].Value) / (temp[i + 1].Key.ToOADate() - temp[i].Key.ToOADate());
                double n = temp[i].Value - (k * temp[i].Key.ToOADate());

                double y3 = k * temp[i + 2].Key.ToOADate() + n;

                if( y3 < 0)
                {
                    y3 = 0;
                }

                Assert.AreEqual(Math.Round(temp[i + 2].Value, 5), Math.Round(y3, 5));
            }
        }
    }
}
