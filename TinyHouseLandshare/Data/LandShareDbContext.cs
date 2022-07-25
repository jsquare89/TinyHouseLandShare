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

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(u => u.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
               .HasOne(m => m.Receiver)
               .WithMany(u => u.ReceivedMessages)
               .HasForeignKey(u => u.ReceiverId)
               .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
