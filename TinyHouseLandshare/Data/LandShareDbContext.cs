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

        public DbSet<LandListing> LandListings { get; set; }
        public DbSet<SeekerListing> SeekerListings { get; set; }

        public DbSet<UserListing> UserSeekerListings { get; set; }
        public DbSet<UserListing> UserLandListings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserListing>().HasKey(u => new { u.User, u.Listing });
        }

    }
}
