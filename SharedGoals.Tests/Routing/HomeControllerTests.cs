using SharedGoals.Controllers;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace SharedGoals.Tests.Routing
{
    public class HomeControllerTests
    {
        [Fact]
        public void GetIndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void GetErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
