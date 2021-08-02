using AutoMapper;
using AutoMapper.QueryableExtensions;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using SharedGoals.Services.Goals.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharedGoals.Services.GoalWorks
{
    public class GoalWorkService : IGoalWorkService
    {
        private readonly SharedGoalsDbContext dbContext;
        private readonly IMapper mapper;

        public GoalWorkService(SharedGoalsDbContext dbContext, IMapper mapper) 
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<GoalWorkServiceModel> Mine(string userId)
            => this.dbContext
                .GoalWorks
                .Where(g => g.UserId == userId)
                .ProjectTo<GoalWorkServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<GoalWorkServiceModel> All()
            => this.dbContext.GoalWorks
                .ProjectTo<GoalWorkServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public bool GoalExists(int id)
            => this.dbContext.Goals.FirstOrDefault(g => g.Id == id) != null;

        public void Work(string description, string userId, int goalId)
        {
            var goalWork = new GoalWork()
            {
                Description = description,
                UserId = userId,
                GoalId = goalId
            };

            dbContext.GoalWorks.Add(goalWork);
            dbContext.SaveChanges();
        }
    }
}
