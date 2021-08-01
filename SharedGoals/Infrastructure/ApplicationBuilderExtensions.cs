using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

using static SharedGoals.Areas.Admin.AdminConstants;

namespace SharedGoals.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedTags(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<SharedGoalsDbContext>();

            data.Database.Migrate();
        }

        private static void SeedTags(IServiceProvider services)
        {
            var data = services.GetRequiredService<SharedGoalsDbContext>();

            if (data.Tags.Any())
            {
                return;
            }

            data.Tags.AddRange(new[]
            {
                new Tag { Name = "Very Important" },
                new Tag { Name = "Important" },
                new Tag { Name = "Neutral" },
                new Tag { Name = "Not much important" }
            });

            data.SaveChanges();
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

                    const string adminEmail = "admin@mail.com";
                    const string adminPassword = "123123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        LastName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
