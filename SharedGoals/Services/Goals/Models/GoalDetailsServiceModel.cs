﻿using SharedGoals.Models.Goals;
using System.Collections.Generic;

namespace SharedGoals.Services.Goals.Models
{
    public class GoalDetailsServiceModel : GoalServiceModel
    {
        public string Description { get; init; }

        public string CreatedOn { get; init; }

        public IEnumerable<GoalWorkServiceModel> GoalWorks { get; set; } = new List<GoalWorkServiceModel>();
    }
}