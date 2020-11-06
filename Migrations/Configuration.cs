namespace ProjectBoard.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjectBoard.Models;
    using System;
    using System.Collections.Generic;
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
            //if (!context.Notifications.Any())
            //{
            //    context.Notifications.AddOrUpdate(x => x.Id,
            //        new Notification { Body = "This is an urgent notification", Notificationtype = Notificationtype.Urgent }
            //        );
            //}

            /*if (!context.Projects.Any())
            {
                context.Projects.AddOrUpdate(x => x.Id,
                new Project { Name = "proj1", StartDate = new DateTime(2020, 02, 01), Deadline = new DateTime(2020, 12, 01), Budget = 500.10, IsCompleted = false, Priority = Priority.medium },
                new Project { Name = "proj2", StartDate = new DateTime(2019, 01, 01), Deadline = new DateTime(2020, 11, 30), Budget = 400000.10, IsCompleted = false, Priority = Priority.high },
                new Project { Name = "proj3", StartDate = new DateTime(2020, 03, 01), Deadline = new DateTime(2021, 12, 01), Budget = 50000.10, IsCompleted = false, Priority = Priority.low },
                new Project { Name = "overdue tasks", StartDate = new DateTime(2020, 08, 01), Deadline = new DateTime(2020, 12, 01), Budget = 6000, IsCompleted = false, Priority = Priority.high },
                new Project { Name = "overdue project", StartDate = new DateTime(2020, 07, 30), Deadline = new DateTime(2020, 09, 01), Budget = 7000.55, IsCompleted = false, Priority = Priority.high }
                );
            }*/

            /*if (!context.Tasks.Any())
            {
                context.Tasks.AddOrUpdate(
                new ATask { Id = 1, Name = "task1-1", Body = "project 1's task 1. not overdue. completed.", StartDate = new DateTime(2020, 02, 15), Deadline = new DateTime(2020, 02, 20), IsCompleted = true, CompletionPerc = 80, Priority = Priority.medium },
                new ATask { Id = 2, Name = "task2-1", Body = "Project 1's task 2. Not overdue. Not completed.", StartDate = new DateTime(2020, 11, 01), Deadline = new DateTime(2020, 11, 30), IsCompleted = false, CompletionPerc = 40, Priority = Priority.high },
                new ATask { Id = 3, Name = "task1-2", Body = "Project 2's task 1. Not overdue. Not completed.", StartDate = new DateTime(2020, 11, 01), Deadline = new DateTime(2020, 11, 30), IsCompleted = false, CompletionPerc = 40, Priority = Priority.high }
                );
            }*/

                if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Manager" };
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
            if (!context.Users.Any(u => u.UserName == "manager@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var PasswordHash = new PasswordHasher();
                var user = new ApplicationUser
                {
                    UserName = "manager@mysite.com",
                    Email = "manager@mysite.com",
                    PasswordHash = PasswordHash.HashPassword("Abc123!")
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Manager");
            }
            if (!context.Users.Any(u => u.UserName == "manager1@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var PasswordHash = new PasswordHasher();
                var user = new ApplicationUser
                {
                    UserName = "manager1@mysite.com",
                    Email = "manager1@mysite.com",
                    PasswordHash = PasswordHash.HashPassword("Abc123!")
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Manager");
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
            if (!context.Users.Any(u => u.UserName == "developer1@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var PasswordHash = new PasswordHasher();
                var user = new ApplicationUser
                {
                    UserName = "developer1@mysite.com",
                    Email = "developer1@mysite.com",
                    PasswordHash = PasswordHash.HashPassword("Abc123!")
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Developer");
            }
            if (!context.Users.Any(u => u.UserName == "developer2@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var PasswordHash = new PasswordHasher();
                var user = new ApplicationUser
                {
                    UserName = "developer2@mysite.com",
                    Email = "developer2@mysite.com",
                    PasswordHash = PasswordHash.HashPassword("Abc123!")
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Developer");
            }
            if (!context.Users.Any(u => u.UserName == "developer3@mysite.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var PasswordHash = new PasswordHasher();
                var user = new ApplicationUser
                {
                    UserName = "developer3@mysite.com",
                    Email = "developer3@mysite.com",
                    PasswordHash = PasswordHash.HashPassword("Abc123!")
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Developer");
            }
        }
    }
}