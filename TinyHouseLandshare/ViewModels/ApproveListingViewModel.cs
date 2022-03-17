using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.ViewModels
{
    public class ApproveListingViewModel
    {
        public IEnumerable<SeekerListing> seekerListings { get; set; }
        public IEnumerable<LandListing> landListings { get; set; }
    }
}
