using System.Linq;
using SharedGoals.Controllers;
using SharedGoals.Data.Models;
using SharedGoals.Models.Creators;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace SharedGoals.Tests.Controllers
{
    public class CreatorsControllerTests
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CreatorsController>
                .Instance(controller => controller.WithUser())
                .Calling(c => c.Become())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
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
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
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
