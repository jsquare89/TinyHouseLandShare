using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.ViewModels
{
    public class UserListingsViewModel
    {
        public SeekerListing SeekerListing { get; set; }
        public IEnumerable<LandListingViewModel> LandListings { get; set; }
    }
}
