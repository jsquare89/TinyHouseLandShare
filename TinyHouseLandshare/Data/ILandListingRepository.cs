using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface ILandListingRepository
    {
        LandListing GetLandListing(Guid id);
        IEnumerable<LandListing> GetAllLandListings();
        IEnumerable<LandListing> GetAllApprovedLandListings();
        IEnumerable<LandListing> GetAllUnApprovedSubmittedLandListings();
        LandListing Add(LandListing LandListing);
        LandListing Update(LandListing LandListingUpdated);
        LandListing Delete(Guid id);
    }
}
