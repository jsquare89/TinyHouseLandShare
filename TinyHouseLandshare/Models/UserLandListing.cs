using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class UserLandListing
    {
        public Guid UserId { get; set; }
        public Guid LandListingId { get; set; }
    }
}
