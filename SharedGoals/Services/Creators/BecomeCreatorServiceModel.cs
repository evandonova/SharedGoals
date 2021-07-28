using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Services.Creators
{
    public class BecomeCreatorServiceModel
    {
        [Required]
        [StringLength(CreatorNameMaxLength, MinimumLength = CreatorNameMinLength)]
        public string Name { get; set; }
    }
}
