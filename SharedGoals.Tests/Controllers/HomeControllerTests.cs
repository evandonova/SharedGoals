using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using Xunit;

using static SharedGoals.Areas.Admin.AdminConstants;

namespace SharedGoals.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectView()
        => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexShouldRedirectReturnCorrectViewWhenUserIsAdmin()
        => MyController<HomeController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdministratorRoleName)))
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction("Index", "Home", new { area = "Admin" });

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
