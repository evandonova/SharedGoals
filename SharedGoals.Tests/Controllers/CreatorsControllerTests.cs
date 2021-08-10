using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Data.Models;
using SharedGoals.Models.Creators;
using SharedGoals.Models.Goals;
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
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Creator Name")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
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
                    .To<GoalsController>(c => c.All(With.Any<AllGoalsQueryModel>())));
    }
}
