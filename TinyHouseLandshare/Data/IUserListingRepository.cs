﻿using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public interface IUserListingRepository
    {
        IEnumerable<Guid> GetUserListings(Guid UserId);
        SeekerListing GetUserSeekerListing(Guid UserId);
        IEnumerable<UserLandListing> GetAllUserListings();
        UserLandListing Add(UserLandListing userListing);
        UserLandListing Update(UserLandListing userListingUpdated);
        UserLandListing Delete(Guid id);
    }
}