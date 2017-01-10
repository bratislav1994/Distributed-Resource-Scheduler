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
    }
}
