using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Migrations;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models
{
    public class SalesDbContext : DbContext
    {

        public SalesDbContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<SalesDbContext, Configuration>());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<OpportunityModel> Opportunities { get; set; }

        public DbSet<AccountModel> Accounts { get; set; }

        public DbSet<ContactModel> Contacts { get; set; }

        public DbSet<CountryModel> Countries { get; set; }

        public DbSet<MasterEntryModel> MasterEntries { get; set; }

        public DbSet<ActivityModel> LogingActivities { get; set; }

        public DbSet<LeadModel> Leads { get; set; }

    }
}