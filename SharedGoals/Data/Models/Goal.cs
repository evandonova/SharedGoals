using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants.Goal;

    public class Goal
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; init; }

        public DateTime DueDate { get; set; }
        
        [Required]
        public string ImageURL { get; set; }

        public bool IsFinished { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; init; }

        public string CreatorId { get; init; }

        public Creator Creator { get; init; }

        public IEnumerable<GoalWork> GoalWorks { get; init; } = new List<GoalWork>();
    }
}
