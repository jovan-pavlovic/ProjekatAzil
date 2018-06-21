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

            context.Breeds.AddOrUpdate(
                new Breed { Name = "Akita" },
                new Breed { Name = "Alaskan Malamute" },
                new Breed { Name = "Basset Hound" },
                new Breed { Name = "Beagle" },
                new Breed { Name = "Bloodhound" },
                new Breed { Name = "Boston Terrier" },
                new Breed { Name = "Chihuahua" },
                new Breed { Name = "Golden Retriever" },
                new Breed { Name = "Jack Russell Terrier" },
                new Breed { Name = "Pug" },
                new Breed { Name = "Maltese" },
                new Breed { Name = "French Bulldog" },
                new Breed { Name = "Bull Terrier" },
                new Breed { Name = "Dalmatian" },
                new Breed { Name = "Bulldog" }
                );

            context.Dogs.AddOrUpdate(
                new Dog { Name = "Bobi", YearOfBirth = 2012, Weight = 3 },
                new Dog { Name = "Marli", YearOfBirth = 2017, Weight = 5 },
                new Dog { Name = "Rex", YearOfBirth = 2005, Weight = 1 },
                new Dog { Name = "Bobi", YearOfBirth = 2016, Weight = 8 },
                new Dog { Name = "Rufus", YearOfBirth = 2012, Weight = 10 },
                new Dog { Name = "Rocco", YearOfBirth = 2001, Weight = 5 },
                new Dog { Name = "Peanut", YearOfBirth = 2005, Weight = 7 },
                new Dog { Name = "Shadow", YearOfBirth = 2018, Weight = 1 },
                new Dog { Name = "Zeus", YearOfBirth = 2015, Weight = 1 },
                new Dog { Name = "Cody", YearOfBirth = 2015, Weight = 7 },
                new Dog { Name = "Buddy", YearOfBirth = 2012, Weight = 5 },
                new Dog { Name = "Bubba", YearOfBirth = 2016, Weight = 1 },
                new Dog { Name = "Leo", YearOfBirth = 2015, Weight = 7 },
                new Dog { Name = "Tank", YearOfBirth = 2016, Weight = 8 },
                new Dog { Name = "Yoda", YearOfBirth = 2012, Weight = 5 },
                new Dog { Name = "Simba", YearOfBirth = 2015, Weight = 8 },
                new Dog { Name = "Coco", YearOfBirth = 2017, Weight = 7 },
                new Dog { Name = "Loki", YearOfBirth = 2014, Weight = 5 },
                new Dog { Name = "Champ", YearOfBirth = 2015, Weight = 8 },
                new Dog { Name = "Buddy", YearOfBirth = 2012, Weight = 5 },
                new Dog { Name = "Moose", YearOfBirth = 2017, Weight = 8 },
                new Dog { Name = "Oscar", YearOfBirth = 2015, Weight = 5 },
                new Dog { Name = "Buddy", YearOfBirth = 2012, Weight = 8 }
                );

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
