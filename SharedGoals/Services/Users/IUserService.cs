using System.Collections.Generic;

namespace SharedGoals.Services.Users
{
    public interface IUserService
    {
        public IEnumerable<UserServiceModel> All();
    }
}
