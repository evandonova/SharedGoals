using System;
using System.ComponentModel;

namespace SharedGoals.Services.Goals.Models
{
    public class GoalServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; init; }

        [DisplayName("Progress In Percents")]
        public int ProgressInPercents { get; init; }

        public string Tag { get; init; }
    }
}
