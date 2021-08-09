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
            int currentPage,
            int totalGoals);

        void Create(
            string name, 
            string description, 
            DateTime dueDate,
            string imageURL,
            int tagId, 
            string creatorId);

        GoalExtendedServiceModel Info(int id);

        GoalDetailsServiceModel Details(int id);

        bool Finish(int id);

        bool Edit(int id,
            string name,
            string description,
            DateTime dueDate,
            string imageURL,
            int tagId);

        bool Delete(int id);

        bool GoalExists(int goalId);

        bool TagExists(int tagId);

        bool DateIsValid(DateTime dueDate);

        string GetCreatorId(int goalId);

        IEnumerable<GoalTagServiceModel> Tags();
    }
}
