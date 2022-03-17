using System.Linq;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class UserSeekerListingRepository : IUserSeekerListingRepository
    {
        private readonly LandShareDbContext _context;

        public UserSeekerListingRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public UserSeekerListing Add(UserSeekerListing userListing)
        {
            _context.UserSeekerListings.Add(userListing);
            _context.SaveChanges();
            return userListing;
        }

        public UserSeekerListing Delete(UserSeekerListing userSeekerListing)
        {
            _context.UserSeekerListings.Remove(userSeekerListing);
            return userSeekerListing;
        }

        public IEnumerable<UserSeekerListing> GetAllUserListings()
        {
            return _context.UserSeekerListings;
        }

        public SeekerListing GetUserListing(Guid UserId)
        {
            var listingId = _context.UserSeekerListings.Where(l => l.UserId == UserId).Select(l => l.SeekerListingId).FirstOrDefault();
            return _context.SeekerListings.Where(l => l.Id == listingId).FirstOrDefault();
        }

        public UserSeekerListing Update(UserSeekerListing userListingUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
