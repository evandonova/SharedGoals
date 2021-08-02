using SharedGoals.Services.Goals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedGoals.Services.GoalWorks
{
    public interface IGoalWorkService
    {
        public IEnumerable<GoalWorkServiceModel> Mine(string userId);
        public IEnumerable<GoalWorkServiceModel> All();
        public bool GoalExists(int id);
        public void Work(string description, string userId, int goalId);
    }
}
