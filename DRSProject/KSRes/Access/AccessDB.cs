using KSRes.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRes.Access
{
    public class AccessDB : DbContext
    {
        public AccessDB() : base("localDB") { }

        public DbSet<ConsuptionHistory> ConsuptionHistory { get; set; }
        public DbSet<ProductionHistroy> ProductionHistory { get; set; }
        public DbSet<RegisteredService> RegisteredServices { get; set; }
    }
}
