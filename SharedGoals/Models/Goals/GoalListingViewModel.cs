using System.ComponentModel;

namespace SharedGoals.Models.Goals
{
    public class GoalListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }
        public string Description { get; init; }

        [DisplayName("Created On Date")]
        public string CreatedOnDate { get; init; }

        [DisplayName("Due Date")]
        public string DueDate { get; init; }

        public string Importance { get; init; }

        [DisplayName("Progress In Percents")]
        public string ProgressInPercents { get; init; }

        public string Owner { get; init; }
    }
}
