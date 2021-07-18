using System.Collections.Generic;

namespace SharedGoals.Models.Goals
{
    public class AllGoalsQueryModel
    {
        public const int CarsPerPage = 4;

        public int CurrentPage { get; init; } = 1;

        public int TotalGoals { get; set; }

        public IEnumerable<GoalListingViewModel> Goals { get; set; } = new List<GoalListingViewModel>();
    }
}
