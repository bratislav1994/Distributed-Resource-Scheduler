using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Interfaces;
using LKRes.Access;
using LKRes.Data;

namespace ActivePowerGenerator
{
    class ActivePowerManagement : IActivePowerManagement
    {
        public Dictionary<string, double> ChangeActivePower(ref UpdateInfo update)
        {
            Random randGenerator = new Random();

            Dictionary<string, double> powerForProcessing = new Dictionary<string, double>();
            foreach (Group groupIterator in update.Groups)
            {
                List<Generator> generetors = update.Generators.Where(gen => gen.GroupID.Equals(groupIterator.MRID)).ToList();
                foreach (Generator generatorIterator in generetors)
                {
                    //samo radi ako je na lokalu
                    if (generatorIterator.SetPoint == -1 && generatorIterator.HasMeasurment)
                    {
                        double newPower = 0;
                        //povecaj za 10%
                        if (randGenerator.Next(0, 2) == 0)
                        {
                            newPower = generatorIterator.ActivePower + (generatorIterator.ActivePower / 10);
                            if (newPower >= generatorIterator.Pmin && newPower <= generatorIterator.Pmax)
                            {
                                generatorIterator.ActivePower = newPower;
                            }
                        }
                        else
                        {
                            newPower = generatorIterator.ActivePower - (generatorIterator.ActivePower / 10);
                            if (newPower >= generatorIterator.Pmin && newPower <= generatorIterator.Pmax)
                            {
                                generatorIterator.ActivePower = newPower;
                            }
                        }
                    }
                }

                //izracujan ukupnu snagu i broj reprezentativnih generatora 
                double totalPower = 0;
                int numberOfGeneratorsWithMeasurments = 0;
                foreach (Generator genIt in generetors)
                {
                    if (genIt.HasMeasurment)
                    {
                        totalPower += genIt.ActivePower;
                        numberOfGeneratorsWithMeasurments++;
                    }
                }

                //postavi snagu ne reprezentativnih generatrora na prosecnu vrednost reprezentativnih generatoa
                double averagePower = totalPower / numberOfGeneratorsWithMeasurments;
                foreach (Generator genIt in generetors)
                {
                    if (!genIt.HasMeasurment && genIt.SetPoint == -1)
                    {
                        genIt.ActivePower = averagePower;
                    }
                }

                foreach (Generator genIt in generetors)
                {
                    powerForProcessing.Add(genIt.MRID, genIt.ActivePower);
                }
            }

            return powerForProcessing;
        }
    }
}
