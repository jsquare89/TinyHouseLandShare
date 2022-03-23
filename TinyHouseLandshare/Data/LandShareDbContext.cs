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

        public DbSet<UserListing> UserListings { get; set; }
        //public DbSet<UserSeekerListing> UserSeekerListings { get; set; }
        //public DbSet<UserLandListing> UserLandListings { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserSeekerListing>().HasKey(u => new { u.UserId, u.SeekerListingId });
            modelBuilder.Entity<UserLandListing>().HasKey(u => new { u.UserId, u.LandListingId });
        }

    }
}
