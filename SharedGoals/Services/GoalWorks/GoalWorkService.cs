using SharedGoals.Data;
using SharedGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedGoals.Services.GoalWorks
{
    public class GoalWorkService : IGoalWorkService
    {
        private readonly SharedGoalsDbContext dbContext;

        public GoalWorkService(SharedGoalsDbContext dbContext)
            => this.dbContext = dbContext;

        public IEnumerable<GoalWorkServiceModel> Mine(string userId)
            => this.dbContext.GoalWorks
                .Where(g => g.UserId == userId)
                .Select(g => new GoalWorkServiceModel()
                {
                    Description = g.Description,
                    WorkDoneInPercents = g.WorkDoneInPercents,
                    User = this.dbContext.Users.FirstOrDefault(u => u.Id == g.UserId).UserName,
                    Goal = this.dbContext.Goals.FirstOrDefault(gl => gl.Id == g.GoalId).Name,
                })
                .ToList();

        public IEnumerable<GoalWorkServiceModel> All()
            => this.dbContext.GoalWorks
                .Select(g => new GoalWorkServiceModel()
                {
                    Description = g.Description,
                    WorkDoneInPercents = g.WorkDoneInPercents,
                    User = this.dbContext.Users.FirstOrDefault(u => u.Id == g.UserId).UserName,
                    Goal = this.dbContext.Goals.FirstOrDefault(gl => gl.Id == g.GoalId).Name,
                })
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
