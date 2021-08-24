using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Models.Comments
{
    public class CommentFormModel
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Body { get; init; }
    }
}
