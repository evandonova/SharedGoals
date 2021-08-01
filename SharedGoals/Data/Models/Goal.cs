using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants;

    public class Goal
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(GoalNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GoalDescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; init; }

        public DateTime DueDate { get; set; }

        public int ProgressInPercents { get; set; } = 0;

        public int TagId { get; set; }

        public Tag Tag { get; init; }

        public string CreatorId { get; init; }

        public Creator Creator { get; init; }

        public IEnumerable<GoalWork> GoalWorks { get; init; } = new List<GoalWork>();
    }
}
