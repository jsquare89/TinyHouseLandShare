using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.ViewModels
{
    public class ApproveListingViewModel
    {
        public IEnumerable<SeekerListingViewModel> seekerListings { get; set; }
        public IEnumerable<LandListingViewModel> landListings { get; set; }
    }
}
