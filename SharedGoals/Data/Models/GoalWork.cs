using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Data.Models
{
    public class GoalWork
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(WorkDescriptionMaxLength)]
        public string Description { get; init; }

        public int WorkDoneInPercents { get; init; }

        [Required]
        public string UserId { get; init; }

        public User User { get; init; }

        public int GoalId { get; init; }

        public Goal Goal { get; init; }
    }
}
