//-----------------------------------------------------------------------
// <copyright file="LocalDB.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes.Access
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KSRes.Data;

    public class LocalDB : ILocalDB
    {
        private static ILocalDB myDB;

        public static ILocalDB Instance
        {
            get
            {
                if (myDB == null)
                {
                    myDB = new LocalDB();
                }

                return myDB;
            }

            set
            {
                if (myDB == null)
                {
                    myDB = value;
                }
            }
        }

        public bool AddConsuption(ConsuptionHistory history)
        {
            using (var access = new AccessDB())
            {
                access.ConsuptionHistory.Add(history);
                int i = access.SaveChanges();

                if (i > 0)
                {
                    return true;
                }

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
                {
                    return true;
                }

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
                List<ProductionHistory> productions = access.ProductionHistory.ToList();
                List<ProductionHistory> temp = new List<ProductionHistory>();

                foreach (ProductionHistory production in productions)
                {
                    if (production.TimeStamp.ToOADate() >= condition.ToOADate())
                    {
                        temp.Add(production);
                    }
                }

                return temp;
            }
        }

        public bool Registration(RegisteredService service)
        {
            using (var access = new AccessDB())
            {
                access.RegisteredServices.Add(service);
                int i = access.SaveChanges();

                if (i > 0)
                {
                    return true;
                }

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

        bool ILocalDB.DeleteRegistrationService(string username)
        {
            using (var access = new AccessDB())
            {
                RegisteredService service = access.RegisteredServices.Where(x => x.Username.Equals(username)).FirstOrDefault();

                if (service != null)
                {
                    access.RegisteredServices.Remove(service);
                    int i = access.SaveChanges();

                    if (i > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
