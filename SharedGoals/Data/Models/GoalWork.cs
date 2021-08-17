using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants.GoalWork;

    public class GoalWork
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; init; }

        [Required]
        public string UserId { get; init; }

        public User User { get; init; }

        public int GoalId { get; init; }

        public Goal Goal { get; init; }
    }
}
