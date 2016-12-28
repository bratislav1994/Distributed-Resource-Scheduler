using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Interfaces;

namespace LKRes.Services
{
    class LKResService : ILKRes
    {
        public string Ping()
        {
            return "OK";
        }

        public void SendSetPoint(List<SetPoint> setPoints)
        {
            throw new NotImplementedException();
        }
    }
}
