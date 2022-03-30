using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Services
{
    public class ListingService : IListingService
    {
        private readonly ISeekerListingRepository _seekerListingRepository;
        private readonly ILandListingRepository _landListingRepository;
        private readonly IUserListingRepository _userListingRepository;

        public ListingService(ISeekerListingRepository seekerListingRepository,
                              ILandListingRepository landListingRepository,
                              IUserListingRepository userListingRepository)
        {
            _seekerListingRepository = seekerListingRepository;
            _landListingRepository = landListingRepository;
            _userListingRepository = userListingRepository;
        }

        public LandListing AddLandListing(LandListing landListing, Guid userId)
        {
            landListing = _landListingRepository.Add(landListing);

            var userListing = new UserListing
            {
                UserId = userId,
                LandListingId = landListing.Id
            };
            _userListingRepository.Add(userListing);

            return landListing;
        }

        public SeekerListing AddSeekerListing(SeekerListing seekerListing, Guid userId)
        {
            seekerListing = _seekerListingRepository.Add(seekerListing);

            var userListing = new UserListing
            {
                UserId = userId,
                SeekerListingId = seekerListing.Id
            };
            _userListingRepository.Add(userListing);

            return seekerListing;
        }

        public void DeleteLandListing(Guid landListingId)
        {
            var listingId = _userListingRepository.GetListingIdBySeekerOrLandListing(landListingId);
            _userListingRepository.Delete(listingId);
            _landListingRepository.Delete(landListingId);
        }

        public void DeleteSeekerListing(Guid seekerListingId)
        {
            var listingId = _userListingRepository.GetListingIdBySeekerOrLandListing(seekerListingId);
            _userListingRepository.Delete(listingId);
            _seekerListingRepository.Delete(seekerListingId);
        }

        public IEnumerable<SeekerListing> GetAllApprovedSeekerListings()
        {
            return _seekerListingRepository.GetAllApprovedSeekerListings();
        }

        public SeekerListing GetSeekerListing(Guid id)
        {
            return _seekerListingRepository.GetSeekerListing(id);
        }

        public IEnumerable<SeekerListing> Search(SeekerSearchFilter seekerSearch)
        {
            return _seekerListingRepository.Search(seekerSearch);
        }

        public LandListing UpdateLandListing(LandListing updatedLandListing)
        {
            return _landListingRepository.Update(updatedLandListing);
        }

        public SeekerListing UpdateSeekerListing(SeekerListing updatedSeekerListing)
        {
            return _seekerListingRepository.Update(updatedSeekerListing);
        }
    }
}
