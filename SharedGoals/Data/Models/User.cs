using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants;
    public class User : IdentityUser
    {
        [MaxLength(UserFirstNameMaxLenght)]
        public string FirstName { get; init; }

        [MaxLength(UserLastNameMaxLenght)]
        public string LastName { get; init; }
    }
}
