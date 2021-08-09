using SharedGoals.Data;
using SharedGoals.Services.Goals.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Models.Goals
{
    using static DataConstants;

    public class GoalFormModel
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

        [Required]
        [Url]
        public string ImageURL { get; set; }

        [Display(Name = "Tag")]
        public int TagId { get; set; }

        public IEnumerable<GoalTagServiceModel> Tags { get; set; }
    }
}
