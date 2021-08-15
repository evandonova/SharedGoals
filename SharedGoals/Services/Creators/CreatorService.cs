using System.Linq;
using SharedGoals.Data;
using SharedGoals.Data.Models;

namespace SharedGoals.Services.Creators
{
    public class CreatorService : ICreatorService
    {
        private readonly SharedGoalsDbContext dbContext;

        public CreatorService(SharedGoalsDbContext dbContext)
            => this.dbContext = dbContext;

        public bool IsCreator(string userId)
            => this.dbContext
                .Creators
                .Any(d => d.UserId == userId);

        public bool IsCreatorByName(string name)
            => this.dbContext
                .Creators
                .Any(d => d.Name == name);

        public string GetCreatorName(string userId)
            => this.dbContext
            .Creators
            .FirstOrDefault(u => u.UserId == userId)
            .Name;

        public string IdByUser(string userId)
            => this.dbContext
                .Creators
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public void Become(string userId, string creatorName)
        {
            var creator = new Creator
            {
                Name = creatorName,
                UserId = userId
            };

            this.dbContext.Creators.Add(creator);
            this.dbContext.SaveChanges();
        }
    }
}
