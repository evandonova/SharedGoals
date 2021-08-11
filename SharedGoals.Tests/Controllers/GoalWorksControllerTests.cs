using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Data.Models;
using SharedGoals.Models.GoalWorks;
using SharedGoals.Services.GoalWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using static SharedGoals.Areas.Admin.AdminConstants;

namespace SharedGoals.Tests.Controllers
{
    public class GoalWorksControllerTests
    {
        [Fact]
        public void DetailsShouldReturnView()
            => MyController<GoalWorksController>
                .Instance(instance => instance
                    .WithUser())
            .Calling(c => c.Mine())
            .ShouldHave()
            .ActionAttributes(atributes => atributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<IEnumerable<GoalWorkServiceModel>>());


        [Theory]
        [InlineData(1)]
        public void GetWorkShouldReturnViewWhenUserAdministrator(int goalId)
            => MyController<GoalWorksController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = goalId
                    }))))
                .Calling(c => c.Work(goalId))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(result => result.WithModelOfType<GoalWorkFormModel>());

        [Theory]
        [InlineData(1, "Goal Work", 2)]
        public void PostWorkShouldReturnRedirectWhenUserAdministrator(
            int id,
            string description,
            int goalId)
            => MyController<GoalWorksController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = goalId
                    }))))
                .Calling(c => c.Work(goalId, new GoalWorkFormModel()
                {
                    Id = id,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests()
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
