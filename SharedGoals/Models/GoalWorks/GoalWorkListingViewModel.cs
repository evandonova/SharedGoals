using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedGoals.Models.GoalWorks
{
    public class GoalWorkListingViewModel
    {
        public string Description { get; init; }

        [Display(Name = "Work Done in Percents")]
        public int WorkDoneInPercents { get; init; }

        public string User { get; init; }

        public string Goal { get; init; }
    }
}
