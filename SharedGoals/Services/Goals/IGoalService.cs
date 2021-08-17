using SharedGoals.Data.Models;
using SharedGoals.Services.Goals.Models;
using System;
using System.Collections.Generic;

namespace SharedGoals.Services.Goals
{
    public interface IGoalService
    {
        GoalQueryServiceModel All(
            int goalsPerPage,
            int currentPage);

        void Create(
            string name, 
            string description, 
            DateTime dueDate,
            string imageURL,
            int tagId, 
            string creatorId);

        GoalDetailsServiceModel Details(int id);

        void Finish(int id);

        void Edit(int id,
            string name,
            string description,
            DateTime dueDate,
            string imageURL,
            int tagId);

        void Delete(int id);

        bool Exists(int goalId);

        bool IsFinished(int goalId);

        bool TagExists(int tagId);

        bool DateIsValid(DateTime dueDate);

        string GetCreatorUserId(int goalId);

        string GetCreatorId(int goalId);

        IEnumerable<GoalTagServiceModel> Tags();
    }
}
