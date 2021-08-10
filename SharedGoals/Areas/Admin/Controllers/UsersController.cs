﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SharedGoals.Services.Users;
using System;
using System.Collections.Generic;

namespace SharedGoals.Areas.Admin.Controllers
{
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
            const string usersCacheKey = "UsersCacheKey";

            var users = this.cache.Get<IEnumerable<UserServiceModel>>(usersCacheKey);

            if(users == null)
            {
                users = this.users.All();

                var cacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(usersCacheKey, users, cacheOptions);
            }

            TempData["adminUserName"] = this.User.Identity.Name;
            return View(users);
        }
    }
}
