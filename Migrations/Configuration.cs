namespace ProjectBoard.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjectBoard.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectBoard.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectBoard.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "admin@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var PasswordHash = new PasswordHasher();
                var user = new ApplicationUser
                {
                    UserName = "admin@mysite.com",
                    Email = "admin@mysite.com",
                    PasswordHash = PasswordHash.HashPassword("Abc123!")
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Developer" };
                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "developer@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var PasswordHash = new PasswordHasher();
                var user = new ApplicationUser
                {
                    UserName = "developer@mysite.com",
                    Email = "developer@mysite.com",
                    PasswordHash = PasswordHash.HashPassword("Abc123!")
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Developer");
            }

        }
    }
}
