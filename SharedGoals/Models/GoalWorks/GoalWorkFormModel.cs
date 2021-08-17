using System.ComponentModel.DataAnnotations;
using SharedGoals.Data;

namespace SharedGoals.Models.GoalWorks
{
    using static DataConstants.GoalWork;
    public class GoalWorkFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }
    }
}
