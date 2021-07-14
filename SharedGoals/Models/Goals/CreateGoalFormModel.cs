using SharedGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Models.Goals
{
    public class CreateGoalFormModel
    {
        [Required]
        [StringLength(GoalNameMaxLength, MinimumLength = GoalNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(GoalDescriptionMaxLength, MinimumLength = GoalDescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = "Due To Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Tag")]
        public int TagId { get; set; }

        public IEnumerable<GoalTagViewModel> Tags { get; set; }
    }
}
