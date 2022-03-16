using System.Linq;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class UserListingRepository : IUserListingRepository
    {
        private readonly LandShareDbContext _context;

        public UserListingRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public UserListing Add(UserListing userListing)
        {
            //_context.UserListings.Add(userListing);
            //_context.SaveChanges();
            //return userListing;
            throw new NotImplementedException();

        }

        public UserListing Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserListing> GetAllUserListings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Guid> GetUserListings(Guid UserId)
        {
            //return _context.UserListings.Where(l => l.User == UserId).Select(l => l.Listing).ToList();
            throw new NotImplementedException();

        }

        public SeekerListing GetUserSeekerListing(Guid UserId)
        {
            //var userListings = GetUserListings(UserId);
            //foreach(var listing in userListings)
            //{
            //    if()
            //}
            //var listingId = _context.UserListings.Where(l)
            //return _context.SeekerListings.Where(s => s.Id == )
            throw new NotImplementedException();
        }

        public UserListing Update(UserListing userListingUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
