using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Services.Users
{
    public class UserServiceModel
    {
        public string Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }

        [Display(Name = "First Name")]
        public string FirstName { get; init; }

        [Display(Name = "Last Name")]
        public string LastName { get; init; }
    }
}
