﻿using AutoMapper;
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
            CreateMap<LandListing, LandListingViewModel>();
            CreateMap<SeekerListing, SeekerListingViewModel>();
            CreateMap<LandListingViewModel, LandListing>();
            CreateMap<SeekerListingViewModel, SeekerListing>();
        }
    }
}
