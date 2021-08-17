using System;
using System.Collections.Generic;
using SharedGoals.Services.GoalWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace SharedGoals.Areas.Admin.Controllers
{
    using static AdminConstants;

    public class GoalWorksController : AdminController
    {
        private readonly IGoalWorkService goalWorks;
        private readonly IMemoryCache cache;

        public GoalWorksController(IGoalWorkService goalWorks, IMemoryCache cache)
        {
            this.goalWorks = goalWorks;
            this.cache = cache;
        }

        [Route("/GoalWorks/All")]
        public IActionResult All()
        {
            const string goalWorksCacheKey = GoalWorksCacheKey;

            var goalWorks = this.cache.Get<IEnumerable<GoalWorkServiceModel>>(goalWorksCacheKey);

            if (goalWorks == null)
            {
                goalWorks = this.goalWorks.All();

                var cacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                this.cache.Set(goalWorksCacheKey, goalWorks, cacheOptions);
            }

            return View(goalWorks);
        }
    }
}
