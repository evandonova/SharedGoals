using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedGoals.Services.GoalWorks
{
    public interface IGoalWorkService
    {
        public IEnumerable<GoalWorkExtendedServiceModel> Mine(string userId);
        public IEnumerable<GoalWorkExtendedServiceModel> All();
        public bool GoalExists(int id);
        public void Work(string description, int workDone, string userId, int goalId);
    }
}
