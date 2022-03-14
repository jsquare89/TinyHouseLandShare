using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class LandShareDbContext: IdentityDbContext<UserEntity, UserRoleEntity, Guid>
    {
        public LandShareDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<LandPost> LandPosts { get; set; }
        public DbSet<SeekerPost> LeaseePosts { get; set; }

    }
}
