using SharedGoals.Controllers;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace SharedGoals.Tests.Routing
{
    public class GoalWorksControllerTests
    {
        [Fact]
        public void GetMineShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/GoalWorks/Mine")
                   .WithMethod(HttpMethod.Get))
               .To<GoalWorksController>(a => a.Mine());

        [Theory]
        [InlineData(1)]
        public void GetWorkShouldBeMapped(int goalId)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/GoalWorks/Work/{goalId}")
                   .WithMethod(HttpMethod.Get))
               .To<GoalWorksController>(a => a.Work(goalId));

        [Theory]
        [InlineData(1)]
        public void PostWorkShouldBeMapped(int goalId)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/GoalWorks/Work/{goalId}")
                   .WithMethod(HttpMethod.Post))
               .To<GoalWorksController>(a => a.Work(goalId));
    }
}
