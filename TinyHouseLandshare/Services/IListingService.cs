using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Services
{
    public interface IListingService
    {
        SeekerListing GetSeekerListing(Guid id);
        IEnumerable<SeekerListing> GetApprovedSeekerListings();
        IEnumerable<SeekerListing> GetAllUnapprovedSubmittedSeekerListings();
        SeekerListing AddSeekerListing(SeekerListing seekerListing, Guid userId);
        SeekerListing UpdateSeekerListing(SeekerListing updatedSeekerListing);
        void DeleteSeekerListing(Guid seekerListingId);
        IEnumerable<SeekerListing> SearchSeekerListings(SeekerSearchFilter seekerSearchFilter);

               
        LandListing GetLandListing(Guid userId);
        IEnumerable<LandListing> GetApprovedLandListings();
        IEnumerable<LandListing> GetAllUnApprovedSubmittedLandListings();
        LandListing AddLandListing(LandListing landListing, Guid userId);
        LandListing UpdateLandListing(LandListing updatedLandListing);
        void DeleteLandListing(Guid landListingId);
        IEnumerable<LandListing> SearchLandListings(LandSearchFilter landSearchFilter);

        Guid GetListingIdBySeekerOrLandListingId(Guid id);

        SeekerListing GetUserSeekerListing(Guid userId);
        IEnumerable<LandListing> GetUserLandListings(Guid userId);
    }
}
