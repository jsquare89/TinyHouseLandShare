namespace TinyHouseLandshare.Data
{
    public interface IUserRepository
    {
        string GetNameById(Guid userId);
    }
}
