using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace CommonLibrary.Interfaces
{
    [ServiceContract]
    public interface IActivePowerManagement
    {
        [OperationContract]
        Dictionary<string, double> ChangeActivePower(ref UpdateInfo update, int randomNumber);
    }
}
