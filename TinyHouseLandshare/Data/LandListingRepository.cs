﻿using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class LandListingRepository : ILandListingRepository
    {
        private readonly LandShareDbContext _context;

        public LandListingRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public LandListing Add(LandListing LandListing)
        {
            throw new NotImplementedException();
        }

        public LandListing Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LandListing> GetAllLandListings()
        {
            return _context.LandListings;
        }

        public LandListing GetLandListing(Guid id)
        {
            return _context.LandListings.Find(id);
        }

        public LandListing Update(LandListing LandListingUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
