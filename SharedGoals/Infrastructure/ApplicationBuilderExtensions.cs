using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using System.Linq;

namespace SharedGoals.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<SharedGoalsDbContext>();

            data.Database.Migrate();

            SeedTags(data);

            return app;
        }

        private static void SeedTags(SharedGoalsDbContext data)
        {
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
    }
}
