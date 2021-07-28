using System.Collections.Generic;

namespace SharedGoals.Services.Goals
{
    public class GoalQueryServiceModel
    {
        public int GoalsPerPage { get; init; }

        public int CurrentPage { get; init; }

        public int TotalGoals { get; init; }

        public IEnumerable<GoalServiceModel> Goals { get; init; }
    }
}
