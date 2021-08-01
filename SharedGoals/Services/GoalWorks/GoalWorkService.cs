using AutoMapper;
using AutoMapper.QueryableExtensions;
using SharedGoals.Data;
using SharedGoals.Data.Models;
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

        public IEnumerable<GoalWorkExtendedServiceModel> Mine(string userId)
            => this.dbContext
                .GoalWorks
                .Where(g => g.UserId == userId)
                .ProjectTo<GoalWorkExtendedServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<GoalWorkExtendedServiceModel> All()
            => this.dbContext.GoalWorks
                .ProjectTo<GoalWorkExtendedServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public bool GoalExists(int id)
            => this.dbContext.Goals.FirstOrDefault(g => g.Id == id) != null;

        public void Work(string description, int workDone, string userId, int goalId)
        {
            var currentUser = this.dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == goalId);
            var goalWork = new GoalWork()
            {
                Description = description,
                WorkDoneInPercents = workDone,
                UserId = currentUser.Id,
                GoalId = goalId
            };

            goal.ProgressInPercents += workDone;

            dbContext.GoalWorks.Add(goalWork);
            dbContext.SaveChanges();
        }
    }
}
