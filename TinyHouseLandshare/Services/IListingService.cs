using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Services
{
    public interface IListingService
    {
        SeekerListing GetSeekerListing(Guid id);
        IEnumerable<SeekerListing> GetAllApprovedSeekerListings();
        SeekerListing AddSeekerListing(SeekerListing seekerListing, Guid userId);
        SeekerListing UpdateSeekerListing(SeekerListing updatedSeekerListing);
        void DeleteSeekerListing(Guid seekerListingId);
        IEnumerable<SeekerListing> Search(SeekerSearchFilter seekerSearch);


        LandListing AddLandListing(LandListing landListing, Guid userId);
        LandListing UpdateLandListing(LandListing updatedLandListing);
        void DeleteLandListing(Guid landListingId);

    }
}
