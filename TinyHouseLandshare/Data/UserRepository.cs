namespace TinyHouseLandshare.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly LandShareDbContext _context;

        public UserRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public string GetNameById(Guid userId)
        {
            var name = _context.Users.Find(userId).Name;

            if (name is null)
            {
                return "";
            }
            return name;
        }
    }
}
