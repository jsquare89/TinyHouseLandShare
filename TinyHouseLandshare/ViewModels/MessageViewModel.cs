namespace TinyHouseLandshare.ViewModels
{
    public class MessageViewModel
    {
        public Guid SenderId { get; set; }
        public Guid ListingId { get; set; }
        public string Message { get; set; }
    }
}
