using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Services
{
    public interface IListingService
    {
        SeekerListing GetSeekerListing(Guid id);
        IEnumerable<SeekerListing> GetApprovedSeekerListings();
        SeekerListing AddSeekerListing(SeekerListing seekerListing, Guid userId);
        SeekerListing UpdateSeekerListing(SeekerListing updatedSeekerListing);
        void DeleteSeekerListing(Guid seekerListingId);
        IEnumerable<SeekerListing> SearchSeekerListings(SeekerSearchFilter seekerSearchFilter);


        LandListing GetLandListing(Guid userId);
        IEnumerable<LandListing> GetApprovedLandListings();
        LandListing AddLandListing(LandListing landListing, Guid userId);
        LandListing UpdateLandListing(LandListing updatedLandListing);
        void DeleteLandListing(Guid landListingId);
        IEnumerable<LandListing> SearchLandListings(LandSearchFilter landSearchFilter);

    }
}
