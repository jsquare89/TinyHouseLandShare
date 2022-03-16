using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface ILandListingRepository
    {
        LandListing GetLandListing(Guid id);
        IEnumerable<LandListing> GetAllLandListings();
        LandListing Add(LandListing LandListing);
        LandListing Update(LandListing LandListingUpdated);
        LandListing Delete(Guid id);
    }
}
