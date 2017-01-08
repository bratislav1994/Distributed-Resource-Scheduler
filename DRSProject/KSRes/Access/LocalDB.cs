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
                access.ConsuptionHistory.Add(history);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }

        public bool AddProductions(ProductionHistroy history)
        {
            using (var access = new AccessDB())
            {
                access.ProductionHistory.Add(history);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }

        public List<ConsuptionHistory> ReadConsuptions(string username)
        {
            using (var access = new AccessDB())
            {
                List<ConsuptionHistory> consuptions = access.ConsuptionHistory.Where(x => x.Username.Equals(username)).ToList();
                return consuptions;
            }
        }

        public List<ProductionHistroy> ReadProductions(string username)
        {
            using (var access = new AccessDB())
            {
                List<ProductionHistroy> productions = access.ProductionHistory.Where(x => x.Username.Equals(username)).ToList();
                return productions;
            }
        }
    }
}
