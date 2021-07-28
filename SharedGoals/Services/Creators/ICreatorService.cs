namespace SharedGoals.Services.Creators
{
    public interface ICreatorService
    {
        public bool IsCreator(string userId);

        public bool IsCreatorByName(string name);

        public string IdByUser(string userId);

        public void Become(string userId, string creatorName);
    }
}
