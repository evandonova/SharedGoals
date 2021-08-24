namespace SharedGoals.Services.Creators
{
    public interface ICreatorService
    {
        public bool IsCreator(string userId);

        public bool CreatorNameExists(string name);

        public string GetCreatorName(string userId);

        public string IdByUser(string userId);

        public void Become(string userId, string creatorName);
    }
}
