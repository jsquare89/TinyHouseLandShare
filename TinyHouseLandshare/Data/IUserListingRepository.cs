using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface IUserListingRepository
    {
        IEnumerable<Guid> GetUserListings(Guid UserId);
        SeekerListing GetUserSeekerListing(Guid UserId);
        IEnumerable<UserListing> GetAllUserListings();
        UserListing Add(UserListing userListing);
        UserListing Update(UserListing userListingUpdated);
        UserListing Delete(Guid id);
    }
}
