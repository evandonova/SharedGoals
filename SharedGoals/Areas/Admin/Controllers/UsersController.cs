using System;
using System.Collections.Generic;
using SharedGoals.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace SharedGoals.Areas.Admin.Controllers
{
    using static AdminConstants;

    public class UsersController : AdminController
    {
        private readonly IUserService users;
        private readonly IMemoryCache cache;

        public UsersController(IUserService users, IMemoryCache cache)
        {
            this.users = users;
            this.cache = cache;
        }

        [Route("Users/All")]
        public IActionResult All()
        {
            const string usersCacheKey = UsersCacheKey;

            var users = this.cache.Get<IEnumerable<UserServiceModel>>(usersCacheKey);

            if(users == null)
            {
                users = this.users.All();

                var cacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                this.cache.Set(usersCacheKey, users, cacheOptions);
            }

            TempData[TempDataAdminUsernameKey] = this.User.Identity.Name;
            return View(users);
        }
    }
}
