using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using LKRes.Data;

namespace LKRes.Access
{
    public class DataBase : IDataBase
    {
        private static IDataBase myDB;

        public static IDataBase Instance
        {
            get
            {
                if (myDB == null)
                    myDB = new DataBase();

                return myDB;
            }
            set
            {
                if (myDB == null)
                    myDB = value;
            }
        }

        #region Add
        public bool AddGenerator(GeneratorEntity newGenerator)
        {
            using (var access = new AccessDB())
            {
                access.GeneratorHistory.Add(newGenerator);
                int i = access.SaveChanges();
                Console.WriteLine("Broj generatora je " + access.GeneratorHistory.Count());
                if (i > 0)
                    return true;
                return false;
            }
        }

        public bool AddGroup(GroupEntity newGroup)
        {
            using (var access = new AccessDB())
            {
                access.GroupHistory.Add(newGroup);
                int i = access.SaveChanges();
                Console.WriteLine("Broj grupa je " + access.GroupHistory.Count());
                if (i > 0)
                    return true;
                return false;
            }
        }

        public bool AddSite(SiteEntity newSite)
        {
            using (var access = new AccessDB())
            {
                access.SiteHistory.Add(newSite);
                int i = access.SaveChanges();
                Console.WriteLine("Broj sajtova je " + access.SiteHistory.Count());
                if (i > 0)
                    return true;
                return false;
            }
        }
        #endregion

        #region Delete
        public void RemoveGenerator(Generator existingGenerator)
        {
            using (var access = new AccessDB())
            {
                GeneratorEntity entity = access.GeneratorHistory.Where(e => e.Gen.MRID.Equals(existingGenerator.MRID)).FirstOrDefault();
                if (entity != null)
                {
                    access.GeneratorHistory.Remove(entity);
                }
            }        
        }

        public void RemoveGroup(Group existingGroup)
        {
            using (var access = new AccessDB())
            {
                GroupEntity entity = access.GroupHistory.Where(e => e.GEntity.MRID.Equals(existingGroup.MRID)).FirstOrDefault();
                if (entity != null)
                {
                    access.GroupHistory.Remove(entity);
                }
            }
        }

        public void RemoveSite(Site existingSite)
        {
            using (var access = new AccessDB())
            {
                SiteEntity entity = access.SiteHistory.Where(e => e.SEntity.MRID.Equals(existingSite.MRID)).FirstOrDefault();
                if (entity != null)
                {
                    access.SiteHistory.Remove(entity);
                }
            }
        }
        #endregion

        #region Update
        public bool UpdateGenerator(Generator updateGenerator)
        {
            using (var access = new AccessDB())
            {
                GeneratorEntity entity = access.GeneratorHistory.Where(g => g.Gen.MRID.Equals(updateGenerator.MRID)).FirstOrDefault();

                if (entity != null)
                {
                    entity.Gen.ActivePower = Math.Round(updateGenerator.ActivePower, 3);
                    entity.Gen.GroupID = updateGenerator.GroupID;
                    entity.Gen.GeneratorType = updateGenerator.GeneratorType;
                    entity.Gen.HasMeasurment = updateGenerator.HasMeasurment;
                    entity.Gen.Pmax = updateGenerator.Pmax;
                    entity.Gen.Pmin = updateGenerator.Pmin;
                    entity.Gen.Price = updateGenerator.Price;
                    entity.Gen.Name = updateGenerator.Name;
                    entity.Gen.BasePoint = updateGenerator.BasePoint;

                    if (entity.Gen.WorkingMode == WorkingMode.REMOTE && updateGenerator.WorkingMode == WorkingMode.LOCAL)
                    {
                        
                        entity.Gen.SetPoint = -1;
                    }
                    else
                    {
                        entity.Gen.SetPoint = updateGenerator.SetPoint;
                    }
                    entity.Gen.WorkingMode = updateGenerator.WorkingMode;
                }

                access.MeasurementHistory.Add(new Measurement()
                {
                    MRID = entity.Gen.MRID,
                    ActivePower = entity.Gen.ActivePower,
                    TimeStamp = DateTime.Now
                });

                int i = access.SaveChanges();
                if (i > 0)
                    return true;
                return false;
            }
        }
        #endregion

        #region Read
        public UpdateInfo ReadData()
        {
            UpdateInfo data = new UpdateInfo();

            using (var access = new AccessDB())
            {
                Console.WriteLine("Broj generatora je " + access.GeneratorHistory.Count());
                foreach (GeneratorEntity g in access.GeneratorHistory)
                {
                    data.Generators.Add(g.Gen);
                }

                Console.WriteLine("Broj grupa je " + access.GroupHistory.Count());
                foreach (GroupEntity g in access.GroupHistory)
                {
                    data.Groups.Add(g.GEntity);
                }

                Console.WriteLine("Broj sajtova je " + access.SiteHistory.Count());
                foreach (SiteEntity s in access.SiteHistory)
                {
                    data.Sites.Add(s.SEntity);
                }
            }
            
            return data;
        }
        #endregion

        #region Measurement
        public bool AddMeasurement(Measurement newMeasurement)
        {
            using (var access = new AccessDB())
            {
                access.MeasurementHistory.Add(newMeasurement);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }

        public SortedDictionary<DateTime, double> GetMeasurements(string mRID)
        {
            SortedDictionary<DateTime, double> returnMeasurements = new SortedDictionary<DateTime, double>();

            using (var access = new AccessDB())
            {
                List<Measurement> mesurements = access.MeasurementHistory.Where(m => m.MRID.Equals(mRID)).ToList();
                foreach (Measurement m in mesurements)
                {
                    returnMeasurements.Add(m.TimeStamp, m.ActivePower);
                }
            }

            return returnMeasurements;
        }
        #endregion
    }
}
