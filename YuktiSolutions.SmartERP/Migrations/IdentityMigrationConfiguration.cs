using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using YuktiSolutions.SmartERP.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace YuktiSolutions.SmartERP.Migrations
{
    internal sealed class IdentityMigrationConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public IdentityMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Roles.Any() == false)
            {
                var store = new RoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(context);
                var manager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(store);

                manager.Create(new IdentityRole { Name = "Admin" });
                manager.Create(new IdentityRole { Name = "Sales Executive" });
                manager.Create(new IdentityRole { Name = "Manager" });

            }

            if (context.Users.Any() == false)
            {

                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                
                //Only create this user during development.It will not be
                //generated in the live system.
                var admin = new Models.ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "info@yuktisolutions.com",
                    UserName = "info@yuktisolutions.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };

                if (manager.Create(admin, "Winner2020@").Succeeded)
                {
                    manager.AddToRole(admin.Id, "Admin");
                }   
            }
        }
    }
}