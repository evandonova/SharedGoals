using System;
using System.Linq;
using SharedGoals.Controllers;
using SharedGoals.Data.Models;
using SharedGoals.Models.Goals;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace SharedGoals.Tests.Controllers
{
    using static Areas.Admin.AdminConstants;
    public class GoalsControllerTests
    {
        [Fact]
        public void GetAllShouldReturnViewWhenAnyGoals()
            => MyController<GoalsController>
            .Instance(instance => instance
                  .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                  {
                      Id = 1,
                      Name = "Goal",
                      Description = "The best goal"
                  }))))
            .Calling(c => c.All(new AllGoalsQueryModel() 
            {  
                CurrentPage = 1
            }))
            .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void GetAllShouldReturnRedirectWhenNotExistingCurrentPage()
            => MyController<GoalsController>
            .Instance()
            .Calling(c => c.All(new AllGoalsQueryModel()
            {
                CurrentPage = 0
            }))
            .ShouldReturn()
            .RedirectToAction("All");

        [Theory]
        [InlineData(
            1,
            "Test Goal",
            "Testing",
            "https://mk0gostrengths4h9kdq.kinstacdn.com/wp-content/uploads/2012/03/Goal-Setting.jpg",
            2)]
        public void GetDetailsShouldReturnView(
            int goalId,
            string name,
            string description,
            string imageUrl,
            int tagId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = goalId,
                        Name = name,
                        Description = description,
                        ImageURL = imageUrl,
                        CreatorId = TestUser.Identifier,
                        TagId = tagId
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
            "Tag",
            "creatorId")]
        public void PostCreateShouldReturnRedirectWhenUserAdministrator(
            string name,
            string description,
            string imageUrl,
            int tagId,
            string tagName,
            string creatorId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId,
                        Name = tagName
                    })))
                    .WithData(d => d.WithSet<Creator>(set => set.Add(new Creator()
                    {
                        Id = creatorId,
                        UserId = TestUser.Identifier
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
        [InlineData(1, "Goal Name", "Goal Description", 2, "creatorId")]
        public void GetDeleteShouldReturnViewWithModelWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId,
            string creatorId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Creator>(set => set.Add(new Creator()
                    {
                        Id = creatorId,
                        UserId = TestUser.Identifier
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId,
                        CreatorId = creatorId
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
        [InlineData(1, "Goal Name", "Goal Description", 2, "creatorId")]
        public void PostDeleteShouldReturnRedirectWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId,
            string creatorId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Creator>(set => set.Add(new Creator()
                    {
                        Id = creatorId,
                        UserId = TestUser.Identifier
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId,
                        CreatorId = creatorId
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
        [InlineData(1, "Goal Name", "Goal Description", 2, "creatorId")]
        public void GetEditShouldReturnViewWithModelWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId,
            string creatorId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Creator>(set => set.Add(new Creator()
                    {
                        Id = creatorId,
                        UserId = TestUser.Identifier
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId,
                        CreatorId = creatorId
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
        [InlineData(
            1,
            "Goal Name",
            "Goal Description",
            "https://mk0gostrengths4h9kdq.kinstacdn.com/wp-content/uploads/2012/03/Goal-Setting.jpg",
            2,
            "New Goal Name",
            "creatorId")]
        public void PostEditShouldReturnRedirectWhenUserAdministrator(
            int id,
            string name,
            string description,
            string imageUrl,
            int tagId,
            string newName,
            string creatorId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Creator>(set => set.Add(new Creator()
                    {
                        Id = creatorId,
                        UserId = TestUser.Identifier
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        ImageURL = imageUrl,
                        TagId = tagId,
                        CreatorId = creatorId
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
        [InlineData(1, "Goal Name", "Goal Description", 2, "creatorId")]
        public void GetFinishShouldReturnViewWithModelWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId,
            string creatorId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Creator>(set => set.Add(new Creator()
                    {
                        Id = creatorId,
                        UserId = TestUser.Identifier
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId,
                        CreatorId = creatorId
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
        [InlineData(1, "Goal Name", "Goal Description", 2, "creatorId")]
        public void PostFinishShouldReturnRedirectWhenUserAdministrator(
            int id,
            string name,
            string description,
            int tagId,
            string creatorId)
            => MyController<GoalsController>
                .Instance(instance => instance
                    .WithUser(u => u.InRole(AdministratorRoleName))
                    .WithData(d => d.WithSet<Tag>(set => set.Add(new Tag()
                    {
                        Id = tagId
                    })))
                    .WithData(d => d.WithSet<Creator>(set => set.Add(new Creator()
                    {
                        Id = creatorId,
                        UserId = TestUser.Identifier
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        TagId = tagId,
                        CreatorId = creatorId
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
