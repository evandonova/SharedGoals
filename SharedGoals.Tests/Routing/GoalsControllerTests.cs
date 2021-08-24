using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Controllers.Models.Goals;
using Xunit;

namespace SharedGoals.Tests.Routing
{
    public class GoalsControllerTests
    {
        [Fact]
        public void GetAllShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Goals/All")
                   .WithMethod(HttpMethod.Get))
               .To<GoalsController>(a => a.All(With.Any<AllGoalsQueryModel>()));

        [Theory]
        [InlineData(1)]
        public void GetDetailsShouldBeMapped(int id)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/Goals/Details/{id}")
                   .WithMethod(HttpMethod.Get))
               .To<GoalsController>(a => a.Details(id));

        [Fact]
        public void GetCreateShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Goals/Create")
                   .WithMethod(HttpMethod.Get))
               .To<GoalsController>(a => a.Create());

        [Fact]
        public void PostCreateShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Goals/Create")
                   .WithMethod(HttpMethod.Post))
               .To<GoalsController>(a => a.Create(With.Any<GoalFormModel>()));

        [Theory]
        [InlineData(1)]
        public void GetDeleteShouldBeMapped(int id)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/Goals/Delete/{id}")
                   .WithMethod(HttpMethod.Get))
               .To<GoalsController>(a => a.Delete(id));

        [Fact]
        public void PostDeleteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Goals/Delete")
                   .WithMethod(HttpMethod.Post))
               .To<GoalsController>(a => a.Delete(With.Any<GoalDetailsViewModel>()));

        [Theory]
        [InlineData(1)]
        public void GetEditShouldBeMapped(int id)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/Goals/Edit/{id}")
                   .WithMethod(HttpMethod.Get))
               .To<GoalsController>(a => a.Edit(id));

        [Theory]
        [InlineData(1)]
        public void PostEditShouldBeMapped(int id)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/Goals/Edit/{id}")
                   .WithMethod(HttpMethod.Post))
               .To<GoalsController>(a => a.Edit(id, With.Any<GoalFormModel>()));

        [Theory]
        [InlineData(1)]
        public void GetFinishShouldBeMapped(int id)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/Goals/Finish/{id}")
                   .WithMethod(HttpMethod.Get))
               .To<GoalsController>(a => a.Finish(id));

        [Fact]
        public void PostFinishShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Goals/Finish")
                   .WithMethod(HttpMethod.Post))
               .To<GoalsController>(a => a.Finish(With.Any<GoalDetailsViewModel>()));
    }
}
