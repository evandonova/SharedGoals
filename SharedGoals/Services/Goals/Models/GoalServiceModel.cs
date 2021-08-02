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

        [DisplayName("Finished")]
        public bool IsFinished { get; init; }

        public string Tag { get; init; }
    }
}
