using AutoMapper;
using AutoMapper.QueryableExtensions;
using SharedGoals.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
