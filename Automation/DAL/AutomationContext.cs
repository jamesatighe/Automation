using Automation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Automation.DAL
{
    public class AutomationContext : DbContext
    {
        public AutomationContext() : base("AutomationContext")
        {
        }

        public DbSet<PowerShell> PowerShell { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SCOM> SCOM { get; set; }
        public DbSet<SCOMAlert> SCOMAlert { get; set; }
        public DbSet<AD> AD { get; set; }
        public DbSet<GPO> GPO { get; set; }
        public DbSet<Cache> Cache { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
       }
    }
}