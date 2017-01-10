using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Interfaces
{
    [ServiceContract]
    public interface ILoadForecast
    {
        [OperationContract]
        SortedDictionary<DateTime, double> LoadForecast(List<KeyValuePair<DateTime, double>> consumptions);
    }
}
