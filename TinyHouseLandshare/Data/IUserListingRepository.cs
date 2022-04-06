using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface IUserListingRepository
    {
        IQueryable<UserListing> GetAllUserListings();
        UserListing Add(UserListing userListing);
        bool Delete(Guid id);
        IQueryable<UserListing> GetUserListings(Guid UserId);
        Guid GetUserIdByListing(Guid listingId);
        IEnumerable<LandListing> GetUserLandListings(Guid userId);
        SeekerListing GetUserSeekerListing(Guid userId);
        Guid GetListingIdBySeekerOrLandListing(Guid Id);
        string GetListingTitleBySeekerOrLandListingId(Guid id);
    }
}
