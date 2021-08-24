using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Controllers.Models.Creators;
using SharedGoals.Data.Models;
using System.Linq;
using Xunit;

namespace SharedGoals.Tests.Controllers
{
    public class CreatorsControllerTests
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CreatorsController>
                .Instance(controller => controller.WithUser())
                .Calling(c => c.Become())
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Creator Name")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirect(
            string creatorName)
            => MyController<CreatorsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Become(new BecomeCreatorFormModel
                {
                    Name = creatorName
                }))
                .ShouldHave()
                .ValidModelState()
                .Data(data => data
                    .WithSet<Creator>(creators => creators
                        .Any(c =>
                            c.Name == creatorName &&
                            c.UserId == TestUser.Identifier)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}
