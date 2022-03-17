using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{ 
    public interface ISeekerListingRepository
    {
        SeekerListing GetSeekerListing(Guid id);
        IEnumerable<SeekerListing> GetAllSeekerListings();
        IEnumerable<SeekerListing> GetAllApprovedSeekerListings();
        IEnumerable<SeekerListing> GetAllUnapprovedSubmittedSeekerListings();
        SeekerListing Add(SeekerListing seekerListing);
        SeekerListing Update(SeekerListing seekerListingUpdated);
        SeekerListing Delete(Guid id);
    }
}
