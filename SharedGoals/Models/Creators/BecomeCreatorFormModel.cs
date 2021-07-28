using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Models.Creators
{
    public class BecomeCreatorFormModel
    {
        [Required]
        [StringLength(CreatorNameMaxLength, MinimumLength = CreatorNameMinLength)]
        public string Name { get; set; }
    }
}
