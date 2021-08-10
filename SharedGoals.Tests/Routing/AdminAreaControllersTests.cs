using MyTested.AspNetCore.Mvc;
using SharedGoals.Areas.Admin.Controllers;
using Xunit;

namespace SharedGoals.Tests.Routing
{
    public class AdminAreaControllersTests
    {
        [Fact]
        public void HomeController_IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void UsersController_GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Users/All")
                    .WithMethod(HttpMethod.Get))
                .To<UsersController>(a => a.All());

        [Fact]
        public void GoalWorksController_GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/GoalWorks/All")
                    .WithMethod(HttpMethod.Get))
                .To<GoalWorksController>(a => a.All());
    }
}
