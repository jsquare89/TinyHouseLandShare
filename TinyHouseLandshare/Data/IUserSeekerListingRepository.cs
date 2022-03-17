using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface IUserSeekerListingRepository
    {
        SeekerListing GetUserListing(Guid UserId);
        IEnumerable<UserSeekerListing> GetAllUserListings();
        UserSeekerListing Add(UserSeekerListing userListing);
        UserSeekerListing Update(UserSeekerListing userListingUpdated);
        UserSeekerListing Delete(UserSeekerListing userListing);
    }
}
