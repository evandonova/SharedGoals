using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Models.GoalWorks;
using SharedGoals.Services.GoalWorks;
using System.Linq;

namespace SharedGoals.Areas.Admin.Controllers
{
    public class GoalWorksController : AdminController
    {
        private readonly IGoalWorkService goalWorks;
        private readonly IMapper mapper;

        public GoalWorksController(IGoalWorkService goalWorks, IMapper mapper)
        {
            this.goalWorks = goalWorks;
            this.mapper = mapper;
        }

        [Route("/GoalWorks/All")]
        public IActionResult All()
        {
            var allGoalWorks = this.goalWorks.All();

            var goalWorks = allGoalWorks
                .Select(g => new GoalWorkListingViewModel()
            {
                Description = g.Description,
                WorkDoneInPercents = g.WorkDoneInPercents,
                User = g.User,
                Goal = g.Goal
            });

            return View(goalWorks);
        }
    }
}
