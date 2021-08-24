using System;
using System.Collections.Generic;
using SharedGoals.Areas.Admin.Controllers;
using SharedGoals.Services.GoalWorks;
using SharedGoals.Services.Users;
using Xunit;
using MyTested.AspNetCore.Mvc;
using SharedGoals.Services.Comments;

namespace SharedGoals.Tests.Controllers
{
    using static Areas.Admin.AdminConstants;
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
        public void UsersController_GetAllShouldReturnCorrectViewWithModel()
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
        public void GoalWorksController_GetAllShouldReturnCorrectViewWithModel()
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

        [Fact]
        public void CommentsController_GetAllShouldReturnCorrectViewWithModel()
            => MyController<CommentsController>
                .Instance()
                .Calling(c => c.All())
                .ShouldReturn()
                .View(result => result.WithModelOfType<IEnumerable<CommentServiceModel>>());
    }
}
