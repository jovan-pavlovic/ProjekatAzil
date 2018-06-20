namespace ProjekatAzil.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjekatAzil.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjekatAzil.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProjekatAzil.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(RolesCfg.ADMIN))
            {
                roleManager.Create(new IdentityRole(RolesCfg.ADMIN));
            }
            if (!roleManager.RoleExists(RolesCfg.USER))
            {
                roleManager.Create(new IdentityRole(RolesCfg.USER));
            }

            var adminEmail = WebConfigurationManager.AppSettings["AdminEmail"];
            var adminPass = WebConfigurationManager.AppSettings["AdminPassword"];

            if (!context.Users.Any(p => p.Email == adminEmail))
            {
                var adminManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var passwordHash = adminManager.PasswordHasher.HashPassword(adminPass);
                var admin = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    PasswordHash = passwordHash,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                context.Users.Add(admin);
                context.SaveChanges();
                adminManager.AddToRole(admin.Id, RolesCfg.ADMIN);
            }

        }
    }
}
