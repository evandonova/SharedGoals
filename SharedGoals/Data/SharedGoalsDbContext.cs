using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedGoals.Data.Models;

namespace SharedGoals.Data
{
    public class SharedGoalsDbContext : IdentityDbContext
    {
        public SharedGoalsDbContext(DbContextOptions<SharedGoalsDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonalGoal> PersonalGoals { get; init; }

        public DbSet<TeamGoal> TeamGoals { get; init; }

        public DbSet<Tag> Tags { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<PersonalGoal>()
                .HasOne(c => c.Tag)
                .WithMany(c => c.PersonalGoals)
                .HasForeignKey(c => c.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<TeamGoal>()
                .HasOne(c => c.Tag)
                .WithMany(c => c.TeamGoals)
                .HasForeignKey(c => c.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
