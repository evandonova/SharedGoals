using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Models.GoalWorks
{
    public class GoalWorkFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(WorkDescriptionMaxLength, MinimumLength = WorkDescriptionMinLength)]
        public string Description { get; init; }

        [Range(PercentsMinValue, PercentsMaxValue)]
        [Display(Name = "Work Done in Percents")]
        public int WorkDoneInPercents { get; init; }
    }
}
