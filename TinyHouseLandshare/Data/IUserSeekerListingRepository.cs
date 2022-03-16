using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface IUserSeekerListingRepository
    {
        SeekerListing GetUserListing(Guid UserId);
        IEnumerable<UserListing> GetAllUserListings();
        UserListing Add(UserListing userListing);
        UserListing Update(UserListing userListingUpdated);
        UserListing Delete(Guid id);
    }
}
