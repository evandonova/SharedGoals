using SharedGoals.Attributes;
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
        private static string dateTimeNow = DateTime.UtcNow.ToString();
        private DateTime dueDate;

        [Required]
        [StringLength(GoalNameMaxLength, MinimumLength = GoalNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(GoalDescriptionMaxLength, MinimumLength = GoalDescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = "Due To Date")]
        [DataType(DataType.Date)]
        [CustomDateValidation]
        public DateTime DueDate 
        { 
            get
            {
                if(this.dueDate == default(DateTime))
                {
                    return DateTime.UtcNow.AddDays(1);
                }
                return this.dueDate;
            } 
            set
            {
                this.dueDate = value;
            }
        }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        [Display(Name = "Tag")]
        public int TagId { get; set; }

        public IEnumerable<GoalTagServiceModel> Tags { get; set; }
    }
}
