using AutoMapper;
using AutoMapper.Features;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Profiles
{
    public class LandShareProfiles: Profile
    {
        public LandShareProfiles()
        {
            // Source -> Target
            CreateMap<LandListing, LandListingViewModel>()
                .ForMember(dst => dst.ListerId,
                           src => src.MapFrom(src => src.UserListing.UserId));
            CreateMap<SeekerListing, SeekerListingViewModel>()
                .ForMember(dst => dst.ListerId,
                           src => src.MapFrom(src => src.UserListing.UserId));
            CreateMap<LandListingViewModel, LandListing>();
            CreateMap<SeekerListingViewModel, SeekerListing>();
        }
    }
}
