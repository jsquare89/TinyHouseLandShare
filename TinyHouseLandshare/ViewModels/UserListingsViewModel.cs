using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.ViewModels
{
    public class UserListingsViewModel
    {
        public SeekerListing SeekerListing { get; set; }
        public IEnumerable<LandListing> LandListings { get; set; }
    }
}
