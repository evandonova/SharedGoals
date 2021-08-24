using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Controllers.Models.GoalWorks;
using SharedGoals.Data.Models;
using SharedGoals.Services.GoalWorks;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SharedGoals.Tests.Controllers
{
    using static Web.Areas.Admin.AdminConstants;
    public class GoalWorksControllerTests
    {
        [Fact]
        public void GetMineShouldReturnViewWithModel()
            => MyController<GoalWorksController>
                .Instance(instance => instance
                    .WithUser())
            .Calling(c => c.Mine())
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<IEnumerable<GoalWorkServiceModel>>());


        [Theory]
        [InlineData(1)]
        public void GetWorkShouldReturnViewWithModelWhenUserAdministrator(int goalId)
            => MyController<GoalWorksController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = goalId
                    }))))
                .Calling(c => c.Work(goalId))
                .ShouldReturn()
                .View(result => result.WithModelOfType<GoalWorkFormModel>());

        [Theory]
        [InlineData(1, "Goal Work Description", 2)]
        public void PostWorkShouldReturnRedirectWhenUserAdministrator(
            int id,
            string description,
            int goalId)
            => MyController<GoalWorksController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = goalId,
                        IsFinished = false
                    }))))
                .Calling(c => c.Work(goalId, new GoalWorkFormModel()
                {
                    Id = id,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .Data(data => data
                    .WithSet<GoalWork>(goalWorks => goalWorks
                        .Any(g => g.Description == description)))
                .TempData(td => td
                    .ContainingEntryWithKey("message"))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Mine");
    }
}
