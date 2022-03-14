using Microsoft.EntityFrameworkCore;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class LandShareDbContext: DbContext
    {
        public LandShareDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<LandPost> LandPosts { get; set; }
        public DbSet<LeaseePost> LeaseePosts { get; set; }

    }
}
