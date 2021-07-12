using SharedGoals.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Models.PersonalGoals
{
    public class AddPersonalGoalFormModel
    {
        [Required]
        [StringLength(GoalNameMaxLength, MinimumLength = GoalNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(GoalDescriptionMaxLength, MinimumLength = GoalDescriptionMinLength)]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public string IconUrl { get; set; }

        public double? ProgressInPercents { get; set; } = 0;

        public int TagId { get; set; }

        public Tag Tag { get; init; }
    }
}
