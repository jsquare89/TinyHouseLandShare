using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class SeekerListingRepository : ISeekerListingRepository
    {
        private readonly LandShareDbContext _context;

        public SeekerListingRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public SeekerListing Add(SeekerListing seekerListing)
        {
            _context.SeekerListings.Add(seekerListing);
            _context.SaveChanges();
            return seekerListing;
        }

        public SeekerListing Delete(Guid id)
        {
            SeekerListing seekerListing = _context.SeekerListings.Find(id);

            if(seekerListing is not null)
            {
                _context.SeekerListings.Remove(seekerListing);
                _context.SaveChanges();
            }
            return seekerListing;
        }

        public SeekerListing GetSeekerListing(Guid id)
        {
            return _context.SeekerListings.Find(id);
        }

        public IEnumerable<SeekerListing> GetAllSeekerListings()
        {
            return _context.SeekerListings;
        }

        public SeekerListing Update(SeekerListing seekerPostUpdated)
        {
            var seekerListing = _context.SeekerListings.Attach(seekerPostUpdated);
            seekerListing.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return seekerPostUpdated;
        }
    }
}
