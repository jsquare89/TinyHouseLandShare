using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.ViewModels
{
    public class UserListingsViewModel
    {
        public SeekerListingViewModel SeekerListing { get; set; }
        public IEnumerable<LandListingViewModel> LandListings { get; set; }
    }
}
