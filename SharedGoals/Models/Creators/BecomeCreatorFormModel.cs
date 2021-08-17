using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Models.Creators
{
    using static Data.DataConstants.Creator;
    public class BecomeCreatorFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
