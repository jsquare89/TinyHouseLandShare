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

        public SeekerListing GetSeekerListing(Guid id)
        {
            return _seekerListingRepository.GetSeekerListing(id);
        }

        public IEnumerable<SeekerListing> GetApprovedSeekerListings()
        {
            return _seekerListingRepository.GetAllApprovedSeekerListings();
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

        public SeekerListing UpdateSeekerListing(SeekerListing updatedSeekerListing)
        {
            return _seekerListingRepository.Update(updatedSeekerListing);
        }

        public void DeleteSeekerListing(Guid seekerListingId)
        {
            var listingId = _userListingRepository.GetListingIdBySeekerOrLandListing(seekerListingId);
            _userListingRepository.Delete(listingId);
            _seekerListingRepository.Delete(seekerListingId);
        }

        public IEnumerable<SeekerListing> SearchSeekerListings(SeekerSearchFilter seekerSearch)
        {
            return _seekerListingRepository.Search(seekerSearch);
        }


        public LandListing GetLandListing(Guid userId)
        {
            return _landListingRepository.GetLandListing(userId);
        }
        public IEnumerable<LandListing> GetApprovedLandListings()
        {
            return _landListingRepository.GetAllApprovedLandListings();
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

        public LandListing UpdateLandListing(LandListing updatedLandListing)
        {
            return _landListingRepository.Update(updatedLandListing);
        }

        public void DeleteLandListing(Guid landListingId)
        {
            var listingId = _userListingRepository.GetListingIdBySeekerOrLandListing(landListingId);
            _userListingRepository.Delete(listingId);
            _landListingRepository.Delete(landListingId);
        }

        public IEnumerable<LandListing> SearchLandListings(LandSearchFilter landSearchFilter)
        {
            return _landListingRepository.Search(landSearchFilter);
        }
    }
}
