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
        public UserListing Add(UserListing userListing)
        {
            _context.UserSeekerListings.Add(userListing);
            _context.SaveChanges();
            return userListing;
        }

        public UserListing Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserListing> GetAllUserListings()
        {
            return _context.UserSeekerListings;
        }

        public SeekerListing GetUserListing(Guid UserId)
        {
            var listingId = _context.UserSeekerListings.Where(l => l.User == UserId).Select(l => l.Listing).FirstOrDefault();
            return _context.SeekerListings.Where(l => l.Id == listingId).FirstOrDefault();
        }

        public UserListing Update(UserListing userListingUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
