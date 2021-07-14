namespace SharedGoals.Models.Goals
{
    public class GoalDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string CreatedOn { get; init; }

        public string DueDate { get; init; }

        public string ProgressInPercents { get; init; }

        public string Tag{ get; init; }

        public string Owner{ get; init; }
    }
}
