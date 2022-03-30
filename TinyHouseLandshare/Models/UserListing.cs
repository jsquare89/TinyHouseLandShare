using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyHouseLandshare.Models
{
    public class UserListing
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserEntityId")]
        public Guid UserId { get; set; }
        public Guid? LandListingId { get; set; } = null;
        public Guid? SeekerListingId { get; set; } = null;


        [Required]
        public UserEntity User { get; set; }
        public SeekerListing SeekerListing { get; set; }
        public LandListing LandListing { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
