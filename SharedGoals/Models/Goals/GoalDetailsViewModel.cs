using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Models.Goals
{
    public class GoalDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string CreatedOn { get; init; }

        [Display(Name = "Due Date")]
        public string DueDate { get; init; }

        [Display(Name = "Progress in Percents")]
        public string ProgressInPercents { get; init; }

        public string Tag{ get; init; }
    }
}
