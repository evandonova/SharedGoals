using System.Collections.Generic;
using SharedGoals.Services.Goals.Models;

namespace SharedGoals.Controllers.Models.Goals
{
    public class AllGoalsQueryModel
    {
        public int GoalsPerPage = 4;

        public int CurrentPage { get; init; } = 1;

        public int TotalGoals { get; set; }

        public IEnumerable<GoalServiceModel> Goals { get; set; } = new List<GoalServiceModel>();
    }
}
