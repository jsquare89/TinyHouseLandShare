namespace TinyHouseLandshare.ViewModels
{
    public class HeadMessageViewModel
    {
        public Guid OriginMessageId { get; set; }
        public Guid MessageId { get; set; }
        public Guid SeekerOrLandListingId { get; set; }
        public string Title { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public bool IsViewed { get; set; }
        public string Value { get; set; }
    }
}
