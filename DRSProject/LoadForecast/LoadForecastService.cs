using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadForecast
{
    public class LoadForecastService : ILoadForecast
    {
        public SortedDictionary<DateTime, double> LoadForecast(List<KeyValuePair<DateTime, double>> consumptions)
        {
            SortedDictionary < DateTime, double> retVal = new SortedDictionary<DateTime, double>();
            double x1 = consumptions[0].Key.ToOADate();
            double y1 = consumptions[0].Value;
            double x2 = consumptions[1].Key.ToOADate();
            double y2 = consumptions[1].Value;

            DateTime data = DateTime.Now;

            for(double i = 0; i < 180; i++)
            {
                double x3 = data.AddMinutes(i).ToOADate();
                double y3 = LinearFunction(x1, x2, y1, y2, x3);

                retVal.Add(DateTime.FromOADate(x3), y3);
            }

            return retVal;
        }

        private double LinearFunction(double x1, double x2, double y1, double y2, double x3)
        {
            var k = (y2 - y1) / (x2 - x1);
            var n = y1 - (k * x1);

            return k * x3 + n;
        }
    }
}
