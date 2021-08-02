using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants;

    public class GoalWork
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(WorkDescriptionMaxLength)]
        public string Description { get; init; }

        [Required]
        public string UserId { get; init; }

        public User User { get; init; }

        public int GoalId { get; init; }

        public Goal Goal { get; init; }
    }
}
