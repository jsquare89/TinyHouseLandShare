namespace TinyHouseLandshare.ViewModels
{
    public class UserListingsViewModel
    {
        public Guid ListingId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
