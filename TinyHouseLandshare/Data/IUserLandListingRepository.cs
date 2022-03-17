using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface IUserLandListingRepository
    {
        IEnumerable<LandListing> GetUserListings(Guid UserId);
        IEnumerable<UserLandListing> GetAllUserListings();
        UserLandListing Add(UserLandListing userListing);
        UserLandListing Update(UserLandListing userListingUpdated);
        UserLandListing Delete(UserLandListing userListing);
    }
}
