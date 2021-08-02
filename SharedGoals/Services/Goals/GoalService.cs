using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using SharedGoals.Services.Goals.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedGoals.Services.Goals
{
    public class GoalService : IGoalService
    {
        private readonly SharedGoalsDbContext dbContext;
        private readonly IMapper mapper;

        public GoalService(SharedGoalsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public GoalQueryServiceModel All(int goalsPerPage, int currentPage, int totalGoals)
        {
            var goals = this.dbContext.Goals
               .Skip((currentPage - 1) * goalsPerPage)
               .Take(goalsPerPage)
               .ProjectTo<GoalServiceModel>(this.mapper.ConfigurationProvider)
               .ToList();

            return new GoalQueryServiceModel()
            {
                GoalsPerPage = goalsPerPage,
                CurrentPage = currentPage,
                TotalGoals = this.dbContext.Goals.Count(),
                Goals = goals
            };
        }
        public void Create(string name, string description,
            DateTime dueDate, int tagId, string creatorId)
        {
            var goalData = new Goal
            {
                Name = name,
                Description = description,
                CreatedOn = DateTime.UtcNow,
                DueDate = dueDate,
                TagId = tagId,
                CreatorId = creatorId
            };

            this.dbContext.Goals.Add(goalData);
            this.dbContext.SaveChanges();
        }
        public GoalExtendedServiceModel Info(int id)
            => this.dbContext
                .Goals
                .Where(g => g.Id == id)
                .ProjectTo<GoalExtendedServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public GoalDetailsServiceModel Details(int id)
        {
            var works = this.dbContext
                .GoalWorks
                .Where(g => g.GoalId == id);

            var goalWorksModel = works.Select(w => new GoalWorkServiceModel()
            {
                Description = w.Description,
                User = this.dbContext.Users.FirstOrDefault(u => u.Id == w.UserId).UserName
            }).ToList();

            return this.dbContext
                .Goals
                .Where(g => g.Id == id)
                .Select(g => new GoalDetailsServiceModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    DueDate = g.DueDate,
                    IsFinished = g.IsFinished,
                    Tag = g.Tag.Name,
                    CreatedOn = g.CreatedOn.ToString("dd/MM/yyyy hh:mm"),
                    GoalWorks = goalWorksModel
                })
                .FirstOrDefault();
        }

        public bool Edit(int id, string name,
            string description,
            DateTime dueDate,
            int tagId)
        {
            var goal = this.dbContext.Goals.Find(id);

            if (goal == null)
            {
                return false;
            }

            goal.Name = name;
            goal.Description = description;
            goal.DueDate = dueDate;
            goal.TagId = tagId;

            this.dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var goal = this.dbContext.Goals.Find(id);

            if (goal == null)
            {
                return false;
            }

            this.dbContext.Remove(goal);
            this.dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<GoalTagServiceModel> Tags()
         => this.dbContext
                .Tags
                .ProjectTo<GoalTagServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public bool TagExists(int tagId)
            => this.dbContext
            .Tags
            .Any(c => c.Id == tagId);

        public bool DateIsValid(DateTime dueDate)
            => dueDate > DateTime.UtcNow &&
                dueDate.Year < 2100;
    }
}
