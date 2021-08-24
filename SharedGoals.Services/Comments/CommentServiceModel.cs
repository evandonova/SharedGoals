namespace SharedGoals.Services.Comments
{
    public class CommentServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Body { get; init; }
        public string User { get; init; }
        public string Goal { get; init; }
    }
}
