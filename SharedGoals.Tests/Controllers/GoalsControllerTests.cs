using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Data.Models;
using SharedGoals.Models.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using static SharedGoals.Areas.Admin.AdminConstants;

namespace SharedGoals.Tests.Controllers
{
    public class GoalsControllerTests
    {
        [Fact]
        public void GetAllShouldReturnView()
            => MyController<GoalsController>
            .Instance()
            .Calling(c => c.All(new AllGoalsQueryModel()))
            .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData(1, "Test Goal", "Testing", "https://mk0gostrengths4h9kdq.kinstacdn.com/wp-content/uploads/2012/03/Goal-Setting.jpg")]
        public void DetailsShouldReturnView(
            int goalId,
            string name,
            string description,
            string imageUrl)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = goalId,
                        Name = name,
                        Description = description,
                        ImageURL = imageUrl
                    }))))
            .Calling(c => c.Details(goalId))
            .ShouldHave()
            .ActionAttributes(atributes => atributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void GetCreateShouldReturnViewWhenUserAdministrator()
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName)))
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(result => result.WithModelOfType<GoalFormModel>());

        [Fact]
        public void GetCreateShouldReturnRedirectWhenUserNotCreatorOrAdministrator()
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser())
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Become", "Creators");

        [Theory]
        [InlineData("Test Goal", 
            "Testing", 
            "https://mk0gostrengths4h9kdq.kinstacdn.com/wp-content/uploads/2012/03/Goal-Setting.jpg",
            1,
            "Tag")]
        public void PostCreateShouldReturnRedirectWhenUserAdministrator(
            string name,
            string description, 
            string imageUrl,
            int tagId,
            string tagName)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId,
                        Name = tagName
                    }))))
                .Calling(c => c.Create(new GoalFormModel()
                { 
                    Name = name,
                    Description = description,
                    DueDate = DateTime.UtcNow.AddDays(5),
                    ImageURL = imageUrl,
                    TagId = tagId
                }))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Goal>(goals => goals
                        .Any(g => g.Name == name &&
                            g.Description == description &&
                            g.ImageURL == imageUrl)))
                .TempData(td => td
                    .ContainingEntryWithKey("message"))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Theory]
        [InlineData(1, "Goal Name", "Goal Description", 2)]
        public void GetDeleteShouldReturnViewWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag() 
                    { 
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId
                    }))))
                .Calling(c => c.Delete(id))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<GoalDetailsViewModel>()
                    .Passing(m => m.Id == id &&
                        m.Name == name &&
                        m.Description == description));

        [Theory]
        [InlineData(1, "Goal Name", "Goal Description", 2)]
        public void PostDeleteShouldReturnRedirectWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId
                    }))))
                .Calling(c => c.Delete(new GoalDetailsViewModel()
                {
                    Id = id
                }))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Goal>(goals => goals
                        .Any(g => g.Name == name &&
                            g.Description == description) == false))
                .TempData(td => td
                    .ContainingEntryWithKey("message"))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Theory]
        [InlineData(1, "Goal Name", "Goal Description", 2)]
        public void GetEditShouldReturnViewWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId
                    }))))
                .Calling(c => c.Edit(id))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<GoalFormModel>()
                    .Passing(m => m.Name == name &&
                        m.Description == description));

        [Theory]
        [InlineData(1, "Goal Name", "Goal Description", "https://mk0gostrengths4h9kdq.kinstacdn.com/wp-content/uploads/2012/03/Goal-Setting.jpg", 2, "New Goal Name")]
        public void PostEditShouldReturnRedirectWhenUserAdministrator(
            int id,
            string name,
            string description,
            string imageUrl,
            int tagId,
            string newName)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        ImageURL = imageUrl,
                        TagId = tagId
                    }))))
                .Calling(c => c.Edit(id, new GoalFormModel()
                {
                    Name = newName,
                    Description = description,
                    ImageURL = imageUrl,
                    DueDate = DateTime.UtcNow.AddDays(3),
                    TagId = tagId
                }))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Goal>(goals => goals
                        .Any(g => g.Name == newName &&
                            g.Description == description)))
                .TempData(td => td
                    .ContainingEntryWithKey("message"))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Theory]
        [InlineData(1, "Goal Name", "Goal Description", 2)]
        public void GetFinishShouldReturnViewWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId
                    }))))
                .Calling(c => c.Finish(id))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<GoalDetailsViewModel>()
                    .Passing(m => m.Id == id &&
                        m.Name == name &&
                        m.Description == description &&
                        m.IsFinished == false));

        [Theory]
        [InlineData(1, "Goal Name", "Goal Description", 2)]
        public void PostFinishShouldReturnRedirectWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId
                    }))))
                .Calling(c => c.Finish(new GoalDetailsViewModel()
                {
                    Id = id
                }))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Goal>(goals => goals
                        .Any(g => g.Name == name &&
                            g.Description == description &&
                            g.IsFinished == true)))
                .TempData(td => td
                    .ContainingEntryWithKey("message"))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
    }
}
