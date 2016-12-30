using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Interfaces;

namespace LKRes.Services
{
    public class LKResService : ILKRes
    {
        public string Ping()
        {
            return "OK";
        }

        public void SendSetPoint(List<SetPoint> setPoints)
        {
            foreach (SetPoint setpoint in setPoints)
            {
                Generator generator = LKForClientService.updateInfo.Generators.Where(gen => gen.MRID.Equals(setpoint.GeneratorID)).FirstOrDefault();
                generator.SetPoint = setpoint.Setpoint;
            }
        }
    }
}
