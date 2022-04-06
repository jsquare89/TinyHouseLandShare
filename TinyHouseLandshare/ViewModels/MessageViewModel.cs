namespace TinyHouseLandshare.ViewModels
{
    public class MessageViewModel
    {

        public Guid SenderId { get; set; }
        public Guid SeekerOrLandListingId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
