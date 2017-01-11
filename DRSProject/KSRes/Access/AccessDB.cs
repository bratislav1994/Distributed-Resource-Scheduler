//-----------------------------------------------------------------------
// <copyright file="AccessDB.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes.Access
{   
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KSRes.Data;

    public class AccessDB : DbContext
    {
        public AccessDB() : base("localDB") { }

        public DbSet<ConsuptionHistory> ConsuptionHistory { get; set; }

        public DbSet<ProductionHistory> ProductionHistory { get; set; }

        public DbSet<RegisteredService> RegisteredServices { get; set; }
    }
}
