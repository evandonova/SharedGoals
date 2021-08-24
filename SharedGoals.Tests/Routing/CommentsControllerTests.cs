using MyTested.AspNetCore.Mvc;
using SharedGoals.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SharedGoals.Tests.Routing
{
    public class CommentsControllerTests
    {
        [Theory]
        [InlineData(1)]
        public void GetWorkShouldBeMapped(int goalId)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/Comments/Add/{goalId}")
                   .WithMethod(HttpMethod.Get))
               .To<CommentsController>(a => a.Add(goalId));

        [Theory]
        [InlineData(1)]
        public void PostWorkShouldBeMapped(int goalId)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath($"/Comments/Add/{goalId}")
                   .WithMethod(HttpMethod.Post))
               .To<CommentsController>(a => a.Add(goalId));
    }
}
