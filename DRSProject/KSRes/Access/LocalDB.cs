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

        public bool AddProductions(ProductionHistory history)
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

        public List<ConsuptionHistory> ReadConsuptions()
        {
            using (var access = new AccessDB())
            {
                List<ConsuptionHistory> consuptions = new List<ConsuptionHistory>();
                consuptions = access.ConsuptionHistory.ToList();

                if (consuptions.Count >= 2)
                {
                    return consuptions.GetRange(consuptions.Count - 2, 2);
                }
                else
                {
                    return consuptions;
                }
            }
        }

        public List<ProductionHistory> ReadProductions(DateTime condition)
        {
            using (var access = new AccessDB())
            {
                List<ProductionHistory> productions = access.ProductionHistory.Where(x => x.TimeStamp.ToOADate() > condition.ToOADate()).ToList();
                
                return productions;
            }
        }

        public bool Registration(RegisteredService service)
        {
            using (var access = new AccessDB())
            {
                access.RegisteredServices.Add(service);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }

        public RegisteredService GetService(string username)
        {
            using (var access = new AccessDB())
            {
                RegisteredService service = access.RegisteredServices.Where(x => x.Username.Equals(username)).FirstOrDefault();
                return service;
            }
        }
    }
}
