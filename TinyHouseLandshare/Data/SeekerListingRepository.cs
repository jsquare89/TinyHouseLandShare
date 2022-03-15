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
        public SeekerListing Add(SeekerListing seekerPost)
        {
            _context.SeekerListings.Add(seekerPost);
            _context.SaveChanges();
            return seekerPost;
        }

        public SeekerListing Delete(Guid id)
        {
            SeekerListing seekerPost = _context.SeekerListings.Find(id);
            if(seekerPost is not null)
            {
                _context.SeekerListings.Remove(seekerPost);
                _context.SaveChanges();
            }
            return seekerPost;
        }

        public SeekerListing GetSeekerPost(Guid id)
        {
            return _context.SeekerListings.Find(id);
        }

        public IEnumerable<SeekerListing> GetAllSeekerPost()
        {
            return _context.SeekerListings;
        }

        public SeekerListing Update(SeekerListing seekerPostUpdated)
        {
            var seekerPost = _context.SeekerListings.Attach(seekerPostUpdated);
            seekerPost.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return seekerPostUpdated;
        }
    }
}
