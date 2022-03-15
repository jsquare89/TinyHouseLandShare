using TinyHouseLandshare.Data;

namespace TinyHouseLandshare.Models
{
    public class SeekerPostRepository : ISeekerPostRepository
    {
        private readonly LandShareDbContext _context;

        public SeekerPostRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public SeekerPost Add(SeekerPost seekerPost)
        {
            _context.SeekerPosts.Add(seekerPost);
            _context.SaveChanges();
            return seekerPost;
        }

        public SeekerPost Delete(int id)
        {
            SeekerPost seekerPost = _context.SeekerPosts.Find(id);
            if(seekerPost is not null)
            {
                _context.SeekerPosts.Remove(seekerPost);
                _context.SaveChanges();
            }
            return seekerPost;
        }

        public SeekerPost GetSeekerPost(int id)
        {
            return _context.SeekerPosts.Find(id);
        }

        public IEnumerable<SeekerPost> GetAllSeekerPost()
        {
            return _context.SeekerPosts;
        }

        public SeekerPost Update(SeekerPost seekerPostUpdated)
        {
            var seekerPost = _context.SeekerPosts.Attach(seekerPostUpdated);
            seekerPost.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return seekerPostUpdated;
        }
    }
}
