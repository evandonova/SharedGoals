using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Controllers.Models.Goals
{
    public class GoalDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string CreatedOn { get; init; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; init; }

        [Display(Name = "Finished")]
        public bool IsFinished { get; init; }

        public string Tag { get; init; }

        public IEnumerable<GoalWorkViewModel> GoalWorks { get; init; } = new List<GoalWorkViewModel>();
    }
}
