using Microsoft.AspNetCore.Mvc;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using System;
using System.Linq;

namespace SharedGoals.Controllers
{
    public class GoalsController : Controller
    {
        private readonly SharedGoalsDbContext data;

        public GoalsController(SharedGoalsDbContext data)
            => this.data = data;

    }
}
