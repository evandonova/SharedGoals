using System;
using System.Linq;
using System.Threading.Tasks;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SharedGoals.Web.Infrastructure
{
    using static Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        private static string adminEmail = "admin@mail.com";
        private static string creatorEmail = "creator@mail.com";
        private static string userEmail = "user@mail.com";

        private static string creatorName = "First Creator";
        private static string adminCreatorName = "Admin";

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedDatabase(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<SharedGoalsDbContext>();

            dbContext.Database.Migrate();
        }

        private static void SeedDatabase(IServiceProvider services)
        {
            SeedTags(services);
            SeedAdministrator(services);
            SeedUsers(services);
            SeedCreator(services);
            SeedGoals(services);
            SeedGoalWorks(services);
        }

        private static void SeedTags(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<SharedGoalsDbContext>();

            if (dbContext.Tags.Any())
            {
                return;
            }

            Task
               .Run(async () =>
               {
                   await dbContext.Tags.AddRangeAsync(new[]
                   {
                       new Tag { Name = "Very Important" },
                       new Tag { Name = "Important" },
                       new Tag { Name = "Neutral" },
                       new Tag { Name = "Not much important" }
                   });

                   await dbContext.SaveChangesAsync();

               })
               .GetAwaiter()
               .GetResult();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminPassword = "pass123#";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        LastName = "Administrator"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedUsers(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();

            Task
                .Run(async () =>
                {
                    const string creatorPassword = "pass123#";

                    var user = new User
                    {
                        Email = creatorEmail,
                        UserName = creatorEmail,
                        FirstName = "Peter",
                        LastName = "Ivanov"
                    };

                    await userManager.CreateAsync(user, creatorPassword);

                })
                .GetAwaiter()
                .GetResult();

            Task
                .Run(async () =>
                {
                    const string userPassword = "pass123#";

                    var user = new User
                    {
                        Email = userEmail,
                        UserName = userEmail,
                        FirstName = "Ivan",
                        LastName = "Tashev"
                    };

                    await userManager.CreateAsync(user, userPassword);

                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedCreator(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<SharedGoalsDbContext>();

            if (dbContext.Creators.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    var userCreatorId = dbContext.Users.FirstOrDefault(x => x.Email == creatorEmail).Id;
                    var userAdminId = dbContext.Users.FirstOrDefault(x => x.Email == adminEmail).Id;

                    await dbContext.Creators.AddRangeAsync(new Creator[]
                    {
                        new Creator()
                        {
                            Name = creatorName,
                            UserId = userCreatorId
                        },
                        new Creator()
                        {
                            Name = adminCreatorName,
                            UserId = userAdminId
                        }
                });
                    await dbContext.SaveChangesAsync();

                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedGoals(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<SharedGoalsDbContext>();

            if (dbContext.Goals.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    var creatorId = dbContext.Creators.FirstOrDefault(x => x.Name == creatorName).Id;
                    var adminCreatorId = dbContext.Creators.FirstOrDefault(x => x.Name == adminCreatorName).Id;

                    await dbContext.Goals.AddRangeAsync(new Goal[]
                    {
                         new Goal()
                         {
                            Name = "Add credit card payment",
                            Description = "Make our app accept creadit card payments.",
                            ImageURL = "https://vervetimes.com/wp-content/uploads/2021/06/credit-card.jpeg",
                            CreatedOn = DateTime.UtcNow,
                            DueDate = DateTime.UtcNow.Date.Add(new TimeSpan(00, 00, 0)).AddMonths(3),
                            CreatorId = creatorId,
                            TagId = 1,
                            IsFinished = true
                        },
                        new Goal()
                        {
                            Name = "Improve team organizational skills",
                            Description = "Our team needs to organize tasks and work better. It needs some training",
                            ImageURL = "https://i.pinimg.com/originals/d6/0f/63/d60f63f6dd27989ce8756c37f774d309.gif",
                            CreatedOn = DateTime.UtcNow.AddDays(-20),
                            DueDate = DateTime.UtcNow.Date.Add(new TimeSpan(00, 00, 0)).AddMonths(3),
                            CreatorId = adminCreatorId,
                            TagId = 2,
                            IsFinished = false
                        }
                    });

                    await dbContext.SaveChangesAsync();

                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedGoalWorks(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<SharedGoalsDbContext>();

            if (dbContext.GoalWorks.Any())
            {
                return;
            }

            Task
               .Run(async () =>
               {
                   var goalId = dbContext.Goals.FirstOrDefault(g => g.TagId == 1).Id;
                   var userId = dbContext.Users.FirstOrDefault(u => u.Email == userEmail).Id;
                   var adminId = dbContext.Users.FirstOrDefault(u => u.Email == adminEmail).Id;

                   await dbContext.GoalWorks.AddRangeAsync(new GoalWork[]
                   {
                       new GoalWork()
                       {
                           Description = "Searched for information on the topic",
                           GoalId = goalId, 
                           UserId = userId
                       },
                       new GoalWork()
                       {
                           Description = "Implemented the functionality",
                           GoalId = goalId,
                           UserId = adminId
                       }
                   });

                   await dbContext.SaveChangesAsync();
               })
               .GetAwaiter()
               .GetResult();
        }
    }
}
