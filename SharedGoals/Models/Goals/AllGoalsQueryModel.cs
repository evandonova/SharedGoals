using SharedGoals.Services.Goals;
using System.Collections.Generic;

namespace SharedGoals.Models.Goals
{
    public class AllGoalsQueryModel
    {
        public int GoalsPerPage = 4;

        public int CurrentPage { get; init; } = 1;

        public int TotalGoals { get; set; }

        public IEnumerable<GoalServiceModel> Goals { get; set; } = new List<GoalServiceModel>();
    }
}
