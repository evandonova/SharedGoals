using System.Linq;
using System.Collections.Generic;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
