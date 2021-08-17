using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SharedGoals.Data.Models
{
    using static DataConstants.User;
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(FirstNameMaxLenght)]
        public string FirstName { get; init; }

        [Required]
        [MaxLength(LastNameMaxLenght)]
        public string LastName { get; init; }
    }
}
