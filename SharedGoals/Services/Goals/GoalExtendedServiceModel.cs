﻿namespace SharedGoals.Services.Goals
{
    public class GoalExtendedServiceModel : GoalServiceModel
    {
        public string Description { get; init; }

        public int TagId { get; init; }

        public string CreatorId { get; init; }

        public string CreatorName { get; init; }

        public string UserId { get; init; }
    }
}