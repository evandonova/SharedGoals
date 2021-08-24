using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Controllers.Models.Comments
{
    using static Data.DataConstants.Comment;
    public class CommentFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght)]
        public string Name { get; init; }

        [Required]
        [StringLength(BodyMaxLenght, MinimumLength = BodyMinLenght)]
        public string Body { get; init; }
    }
}
