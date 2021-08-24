using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using SharedGoals.Controllers.Models.Comments;
using SharedGoals.Data.Models;
using System.Linq;
using Xunit;

namespace SharedGoals.Tests.Controllers
{
    using static Web.Areas.Admin.AdminConstants;
    public class CommentsControllerTests
    {
        [Theory]
        [InlineData(1, 2, "creatorId")]
        public void GetAddShouldReturnViewWithModelWhenUserAdministrator(
            int id,
            int tagId,
            string creatorId)
            => MyController<CommentsController>
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
                        Name = "Goal Name",
                        Description = "Goal Description",
                        TagId = tagId,
                        CreatorId = creatorId
                    }))))
                .Calling(c => c.Add(id))
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<CommentFormModel>());

        [Theory]
        [InlineData(1, "Test Goal", "Testing", 1, "Tag", "creatorId")]
        public void PostAddShouldReturnRedirectWhenUserAdministrator(
            int id,
            string name,
            string body,
            int tagId,
            string tagName,
            string creatorId)
            => MyController<CommentsController>
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
                    })))
                    .WithData(d => d.WithSet<Goal>(set => set.Add(new Goal()
                    {
                        Id = id,
                        Name = "Goal Name",
                        Description = "Goal Description",
                        TagId = tagId,
                        CreatorId = creatorId
                    }))))
                .Calling(c => c.Add(id, new CommentFormModel()
                {
                    Name = name,
                    Body = body
                }))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Comment>(comments => comments
                        .Any(c => c.Name == name && c.Body == body)))
                .TempData(td => td
                    .ContainingEntryWithKey("message"))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Details", "Goals", new { id = id});
    }
}
