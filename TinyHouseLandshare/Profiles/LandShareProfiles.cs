using AutoMapper;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Profiles
{
    public class LandShareProfiles: Profile
    {
        public LandShareProfiles()
        {
            // Source -> Target
            CreateMap<LandListing, LandListingViewModel>();
        }
    }
}
