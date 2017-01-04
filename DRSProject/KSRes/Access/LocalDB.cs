using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSRes.Data;

namespace KSRes.Access
{
    public class LocalDB : ILocalDB
    {
        private static ILocalDB myDB;

        public static ILocalDB Instance
        {
            get
            {
                if (myDB == null)
                    myDB = new LocalDB();

                return myDB;
            }
            set
            {
                if (myDB == null)
                    myDB = value;
            }
        }

        public bool AddConsuption(ConsuptionHistory history)
        {
            using (var access = new AccessDB())
            {
                access.History.Add(history);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }
    }
}
