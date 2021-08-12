using System.Collections.Generic;

namespace SharedGoals.Services.Users
{
    public interface IUserService
    {
        public IEnumerable<UserServiceModel> All();
        public string GetFirstName(string userId);
    }
}
