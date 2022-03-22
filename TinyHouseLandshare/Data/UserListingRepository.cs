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

        public IQueryable<UserListing> GetAllUserListings()
        {
            var listings = _context.UserSeekerListings.
                Select(seekerListings => new UserListing
            {
                UserId = seekerListings.UserId,
                ListingId = seekerListings.SeekerListingId
            }).Union(_context.UserLandListings.
                Select(landListings => new UserListing
                {
                    UserId = landListings.UserId,
                    ListingId = landListings.LandListingId
                }));
            return listings;
        }

        public IQueryable<UserListing> GetUserListings(Guid UserId)
        {
            var listings = GetAllUserListings().
                Where(listing => listing.UserId.Equals(UserId));
            return listings;
        }

        public Guid GetUserIdByListing(Guid listingId)
        {
            var userId = GetAllUserListings().
                Where(listing => listing.ListingId.Equals(listingId)).
                Select(listing => listing.UserId).FirstOrDefault();
            return userId;
        }
    }
}
