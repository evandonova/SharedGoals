using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Services.GoalWorks
{
    public class GoalWorkServiceModel
    {
        public string Description { get; init; }

        [Display(Name = "Work Done in Percents")]
        public int WorkDoneInPercents { get; init; }

        public string User { get; init; }

        public string Goal { get; init; }
    }
}
