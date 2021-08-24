using System.Linq;
using System.Collections.Generic;
using SharedGoals.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace SharedGoals.Services.Users
{
    public class UserService : IUserService
    {
        private readonly SharedGoalsDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(SharedGoalsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<UserServiceModel> All()
            => this
            .dbContext
            .Users
            .ProjectTo<UserServiceModel>(this.mapper.ConfigurationProvider)
            .ToList();

        public string GetFirstName(string userId)
        {
            var user = this.dbContext.Users.Find(userId);

            if(user == null)
            {
                return "Guest";
            }
            else
            {
                return user.FirstName;
            }
        }
    }
}
