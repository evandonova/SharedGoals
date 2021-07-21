using Microsoft.AspNetCore.Identity;
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

        public DbSet<Goal> Goals { get; init; }

        public DbSet<Tag> Tags { get; init; }

        public DbSet<Creator> Creators { get; init; }

        public DbSet<GoalWork> GoalWorks { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Goal>()
                .HasOne(g => g.Tag)
                .WithMany(t => t.Goals)
                .HasForeignKey(g => g.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<GoalWork>()
                .HasOne(gw => gw.Goal)
                .WithMany(g => g.GoalWorks)
                .HasForeignKey(gw => gw.GoalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Goal>()
                .HasOne(g => g.Creator)
                .WithMany(c => c.Goals)
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Creator>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Creator>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
