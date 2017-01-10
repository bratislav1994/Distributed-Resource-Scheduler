using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LKRes.Data;

namespace LKRes.Access
{
    public class AccessDB : DbContext
    {
        public AccessDB() : base("LocalControlerDB") { }

        public DbSet<GeneratorEntity> GeneratorHistory { get; set; }
        public DbSet<GroupEntity> GroupHistory { get; set; }
        public DbSet<SiteEntity> SiteHistory { get; set; }
    }
}
