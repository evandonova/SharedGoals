using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Models.Creators;
using Xunit;

namespace SharedGoals.Tests.Routing
{
    public class CreatorsControllerTests
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Creators/Become")
                   .WithMethod(HttpMethod.Get))
               .To<CreatorsController>(a => a.Become(With.Any<BecomeCreatorFormModel>()));

        [Fact]
        public void PostBecomeShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap(request => request
                       .WithPath("/Creators/Become")
                       .WithMethod(HttpMethod.Post))
                   .To<CreatorsController>(a => a.Become(With.Any<BecomeCreatorFormModel>()));
    }
}
