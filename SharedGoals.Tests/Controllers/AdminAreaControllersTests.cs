using MyTested.AspNetCore.Mvc;
using SharedGoals.Areas.Admin.Controllers;
using SharedGoals.Services.Goals.Models;
using SharedGoals.Services.Users;
using System.Collections.Generic;
using Xunit;

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

        // Add memory cache check
        // Should be for authorized (attribute check)
        [Fact]
        public void UsersController_GetAllShouldReturnCorrectViewWithModelType()
            => MyController<UsersController>
                .Instance()
                .Calling(c => c.All())
                .ShouldHave()
                .ValidModelState()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey("adminUserName"))
                .AndAlso()
                .ShouldReturn()
                .View(c => c.WithModelOfType<IEnumerable<UserServiceModel>>());

        [Fact]
        public void GoalWorksController_GetAllShouldReturnCorrectViewWithModelType()
            => MyController<GoalWorksController>
                .Instance()
                .Calling(c => c.All())
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(c => c.WithModelOfType<IEnumerable<GoalWorkServiceModel>>());
    }
}
