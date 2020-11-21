namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");

            /*Roles for Submitter, Admin, Developer and Manager*/
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Developer" };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Project Manager" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Submitter" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            /*------------------------------------------------------------------------------------------------------*/
            /*Creating and adding one users to roles*/
            var PasswordHash = new PasswordHasher();
            var user1 = new ApplicationUser
            {
                UserName = "Admin@admin.com",
                Email = "Admin@admin.com",
                PasswordHash = PasswordHash.HashPassword("123456"),
            };
            if (!context.Users.Any(u => u.UserName == "Admin@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                manager.Create(user1);
                manager.AddToRole(user1.Id, "Admin");
            }

            var user2 = new ApplicationUser
            {
                UserName = "manager@manager.com",
                Email = "manager@manager.com",
                PasswordHash = PasswordHash.HashPassword("123456"),
            };
            if (!context.Users.Any(u => u.UserName == "manager@manager.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                manager.Create(user2);
                manager.AddToRole(user2.Id, "Project Manager");
            }

            var user3 = new ApplicationUser
            {
                UserName = "developer@developer.com",
                Email = "developer@developer.com",
                PasswordHash = PasswordHash.HashPassword("123456"),
            };
            if (!context.Users.Any(u => u.UserName == "developer@developer.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                manager.Create(user3);
                manager.AddToRole(user3.Id, "Developer");
            }

            var user4 = new ApplicationUser
            {
                UserName = "submitter@submitter.com",
                Email = "submitter@submitter.com",
                PasswordHash = PasswordHash.HashPassword("123456"),
            };
            if (!context.Users.Any(u => u.UserName == "submitter@submitter.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                manager.Create(user4);
                manager.AddToRole(user4.Id, "Submitter");
            }

            /*------------------------------------------------------------------------------------------------------*/
            //Creating Projects, Tickets, TicketTypes, TicketStaus, TicketPriorities
            var priority1 = new TicketPriority
            {
                id = 1,
                Name = "High",
            };
            var priority2 = new TicketPriority
            {
                id = 2,
                Name = "Normal",
            };
            var priority3 = new TicketPriority
            {
                id = 3,
                Name = "Low",
            };

            context.TicketPriorities.AddOrUpdate(priority1);
            context.TicketPriorities.AddOrUpdate(priority2);
            context.TicketPriorities.AddOrUpdate(priority3);

            TicketType[] ticketTypes =
            {
                new TicketType{ id=1,Name="Output Error"},
                new TicketType{ id=2,Name="Unexpected Input"},
                new TicketType{ id=3,Name="Optimize Advice"},
            };
            context.TicketTypes.AddOrUpdate(t => t.Name, ticketTypes);

            TicketStatus[] ticketStatuses =
            {
                new TicketStatus{ id=1,Name="Unassigned"},
                new TicketStatus{ id=2,Name="Assigned"},
                new TicketStatus{ id=3,Name="Completed"},
            };
            context.TicketStatuses.AddOrUpdate(t => t.Name, ticketStatuses);

            Project[] projects =
            {
                new Project{Id = 1, Name = "P1"},
                new Project{Id = 2, Name = "p2"},
            };
            context.Projects.AddOrUpdate(p => p.Name, projects);

            var users = new List<ApplicationUser>();
            users.Add(user1);
            users.Add(user2); 
            users.Add(user3);
            users.Add(user4);
            Ticket[] tickets =
            {
                new Ticket
                {
                    Id = 1,
                    Titile = "Ticket1",
                    Description = "hello world",
                    ProjectId = 1,
                    TicketTypeId = 3,
                    TicketPriorityId =1,
                    TicketStatusId =1,
                    Created= DateTime.Now,
                    OwnerUserId = users.Find(i=>i.UserName=="submitter@submitter.com").Id,
                    AssignedToUserId = users.Find(i=>i.UserName=="developer@developer.com").Id,
                },

                new Ticket
                {
                    Id = 2,
                    Titile = "Ticket2",
                    Description = "this one is assigned",
                    ProjectId = 1,
                    TicketTypeId = 3,
                    TicketPriorityId =1,
                    TicketStatusId =1,
                    OwnerUserId = users.Find(i=>i.UserName=="submitter@submitter.com").Id,
                    AssignedToUserId = users.Find(i=>i.UserName=="developer@developer.com").Id,
                    Created= DateTime.Now,
                    Updated = DateTime.Now
                },

                new Ticket
                {
                    Id = 2,
                    Titile = "Ticket3",
                    Description = "this one is assigned",
                    ProjectId = 2,
                    TicketTypeId = 3,
                    TicketPriorityId =2,
                    TicketStatusId =3,
                    OwnerUserId = users.Find(i=>i.UserName=="submitter@submitter.com").Id,
                    AssignedToUserId = users.Find(i=>i.UserName=="developer@developer.com").Id,
                    Created= DateTime.Now,
                    Updated = DateTime.Now
                },
            };
            context.Tickets.AddOrUpdate(t => t.Titile, tickets);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
