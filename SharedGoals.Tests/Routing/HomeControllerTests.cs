using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using Xunit;

namespace SharedGoals.Tests.Routing
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
