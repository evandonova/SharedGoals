using SharedGoals.Data;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Models.GoalWorks
{
    using static DataConstants;
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
