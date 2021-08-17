﻿using System;
using System.Linq;
using System.Collections.Generic;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using SharedGoals.Services.Goals.Models;
using SharedGoals.Services.GoalWorks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
            CheckGoals();
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
            DateTime dueDate, string imageURL, int tagId, string creatorId)
        {
            var goalData = new Goal
            {
                Name = name,
                Description = description,
                CreatedOn = DateTime.UtcNow,
                DueDate = dueDate,
                IsFinished = false,
                ImageURL = imageURL,
                TagId = tagId,
                CreatorId = creatorId
            };

            this.dbContext.Goals.Add(goalData);
            this.dbContext.SaveChanges();
        }

        public GoalDetailsServiceModel Details(int id)
        {
            var goal = this.dbContext
                  .Goals
                  .ProjectTo<GoalDetailsServiceModel>(this.mapper.ConfigurationProvider)
                  .FirstOrDefault(g => g.Id == id);

            var goalWorksModel =
                this.dbContext
                .GoalWorks
                .Where(g => g.GoalId == id)
                .ProjectTo<GoalWorkServiceModel>(this.mapper.ConfigurationProvider);

            if (goalWorksModel != null)
            {
                goal.GoalWorks = goalWorksModel;
            }

            return goal;
        }

        public void Finish(int id)
        {
            var goal = this.dbContext.Goals.Find(id);
            goal.IsFinished = true;
            this.dbContext.SaveChanges();
        }

        public void Edit(int id, string name,
            string description,
            DateTime dueDate,
            string imageURL,
            int tagId)
        {
            var goal = this.dbContext.Goals.Find(id);

            goal.Name = name;
            goal.Description = description;
            goal.DueDate = dueDate;
            goal.ImageURL = imageURL;
            goal.TagId = tagId;

            this.dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var goal = this.dbContext.Goals.Find(id);

            this.dbContext.Remove(goal);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<GoalTagServiceModel> Tags()
         => this.dbContext
                .Tags
                .ProjectTo<GoalTagServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public bool Exists(int goalId)
            => this.dbContext
            .Goals
            .Any(g => g.Id == goalId);

        public bool IsFinished(int goalId)
             => this.dbContext.Goals.Find(goalId).IsFinished;

        public bool TagExists(int tagId)
            => this.dbContext
            .Tags
            .Any(c => c.Id == tagId);

        public bool DateIsValid(DateTime dueDate)
            => dueDate > DateTime.UtcNow &&
                dueDate.Year < 2100;

        public string GetCreatorUserId(int goalId)
        {
            var creatorId = GetCreatorId(goalId);
            var creator = this.dbContext.Creators.Find(creatorId);
            return creator.UserId;
        }

        public string GetCreatorId(int goalId)
             => this.dbContext.Goals.Find(goalId).CreatorId;

        private void CheckGoals()
        {
            var unfinishedGoals = this.dbContext.Goals.Where(g => !g.IsFinished);

            foreach (var goal in unfinishedGoals)
            {
                if (DateTime.Compare(goal.DueDate, DateTime.UtcNow) <= 0)
                {
                    goal.IsFinished = true;
                }
            }

            this.dbContext.SaveChanges();
        }
    }
}
