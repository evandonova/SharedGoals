using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants.Comment;

    public class Comment
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght)]
        public string Name { get; set; }

        [Required]
        [StringLength(BodyMaxLenght, MinimumLength = BodyMinLenght)]
        public string Body { get; set; }

        [Required]
        public string UserId { get; init; }

        public User User { get; init; }

        public int GoalId { get; init; }

        public Goal Goal { get; init; }
    }
}
