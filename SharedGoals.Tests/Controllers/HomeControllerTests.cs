using SharedGoals.Controllers;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace SharedGoals.Tests.Controllers
{
    using static Web.Areas.Admin.AdminConstants;
    public class HomeControllerTests
    {
        [Fact]
        public void GetIndexShouldReturnCorrectView()
        => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void GetIndexShouldRedirectReturnCorrectViewWhenUserIsAdmin()
        => MyController<HomeController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdministratorRoleName)))
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction("Index", "Home", new { area = "Admin" });

        [Fact]
        public void GetErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
