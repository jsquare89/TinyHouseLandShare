namespace TinyHouseLandshare.Models
{
    public class UserListing
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? LandListingId { get; set; } = null;
        public Guid? SeekerListingId { get; set; } = null;
    }
}
