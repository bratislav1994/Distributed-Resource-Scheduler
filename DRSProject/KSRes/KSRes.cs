using CommonLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;

namespace KSRes
{
    public class KSRes : IKSRes
    {
        public void Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Registration(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void SendMeasurement(Dictionary<string, double> measurments)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateInfo update)
        {
            throw new NotImplementedException();
        }
    }
}
