namespace TinyHouseLandshare.ViewModels
{
    public class ReplyMessageViewModel
    {
        public Guid OriginMessageId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid UserListingId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
