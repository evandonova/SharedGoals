using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharedGoals.Data;
using SharedGoals.Attributes;
using SharedGoals.Services.Goals.Models;

namespace SharedGoals.Models.Goals
{
    using static DataConstants.Goal;
    public class GoalFormModel
    {
        private DateTime dueDate;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
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
        public string ImageURL { get; set; }

        [Display(Name = "Tag")]
        public int TagId { get; set; }

        public IEnumerable<GoalTagServiceModel> Tags { get; set; }
    }
}
