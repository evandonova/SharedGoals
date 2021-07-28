using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(UserFirstNameMaxLenght)]
        public string FirstName { get; init; }

        [MaxLength(UserLastNameMaxLenght)]
        public string LastName { get; init; }
    }
}
