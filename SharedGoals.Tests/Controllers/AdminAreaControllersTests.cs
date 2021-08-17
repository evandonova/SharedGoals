using MyTested.AspNetCore.Mvc;
using SharedGoals.Areas.Admin.Controllers;
using SharedGoals.Services.Goals.Models;
using SharedGoals.Services.GoalWorks;
using SharedGoals.Services.Users;
using System;
using System.Collections.Generic;
using Xunit;

using static SharedGoals.Areas.Admin.AdminConstants;

namespace SharedGoals.Tests.Controllers
{
    public class AdminAreaControllersTests
    {
        [Fact]
        public void HomeController_IndexShouldReturnCorrectView()
            => MyController<HomeController>
            .Instance()
               .Calling(c => c.Index())
               .ShouldReturn()
               .View();

        [Fact]
        public void UsersController_GetAllShouldReturnCorrectViewWithModelType()
            => MyController<UsersController>
                .Instance()
                .Calling(c => c.All())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(UsersCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(5))
                        .WithValueOfType<List<UserServiceModel>>()))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey("adminUserName"))
                .AndAlso()
                .ShouldReturn()
                .View(result => result.WithModelOfType<IEnumerable<UserServiceModel>>());

        [Fact]
        public void GoalWorksController_GetAllShouldReturnCorrectViewWithModelType()
            => MyController<GoalWorksController>
                .Instance()
                .Calling(c => c.All())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(GoalWorksCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(5))
                        .WithValueOfType<List<GoalWorkServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(result => result.WithModelOfType<IEnumerable<GoalWorkServiceModel>>());
    }
}
