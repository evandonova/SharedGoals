﻿using System;
using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Data.Models
{
    public class TeamGoal
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(GoalNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GoalDescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public string IconUrl { get; set; }

        public double? ProgressInPercents { get; set; } = 0;

        public int TagId { get; set; }

        public Tag Tag { get; init; }
    }
}
