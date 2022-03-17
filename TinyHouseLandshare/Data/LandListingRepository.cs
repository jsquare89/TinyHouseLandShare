using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class LandListingRepository : ILandListingRepository
    {
        private readonly LandShareDbContext _context;

        public LandListingRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public LandListing Add(LandListing landListing)
        {
            _context.LandListings.Add(landListing);
            _context.SaveChanges();
            return landListing;
        }

        public LandListing Delete(Guid id)
        {
            var landListing = _context.LandListings.Find(id);
            if (landListing is not null)
            {
                _context.LandListings.Remove(landListing);
                _context.SaveChanges();
            }
            return landListing;
        }

        public IEnumerable<LandListing> GetAllLandListings()
        {
            return _context.LandListings;
        }

        public IEnumerable<LandListing> GetAllApprovedLandListings()
        {
            return _context.LandListings.Where(landListing => landListing.Approved.Equals(true));
        }

        public IEnumerable<LandListing> GetAllUnApprovedSubmittedLandListings()
        {
            return _context.LandListings.Where(landListing => landListing.Approved.Equals(false) && landListing.Submitted.Equals(true));
        }

        public LandListing GetLandListing(Guid id)
        {
            return _context.LandListings.Find(id);
        }

        public LandListing Update(LandListing landListingUpdated)
        {
            var landListing = _context.LandListings.Attach(landListingUpdated);
            landListing.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return landListingUpdated;
        }
    }
}
