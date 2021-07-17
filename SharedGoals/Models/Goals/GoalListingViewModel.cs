using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Models.Goals
{
    public class GoalListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        [DisplayName("Due Date")]
        public string DueDate { get; init; }

        [DisplayName("Progress In Percents")]
        public string ProgressInPercents { get; init; }

        public string Tag { get; init; }
    }
}
