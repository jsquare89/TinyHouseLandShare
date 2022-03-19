using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface IUserListingRepository
    {
        IQueryable<UserListing> GetAllUserListings();
        IQueryable<UserListing> GetUserListings(Guid UserId);
        Guid GetUserIdByListing(Guid listingId);
    }
}
