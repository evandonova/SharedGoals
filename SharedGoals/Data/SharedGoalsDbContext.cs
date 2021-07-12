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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Goal>()
                .HasOne(c => c.Tag)
                .WithMany(c => c.Goals)
                .HasForeignKey(c => c.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
