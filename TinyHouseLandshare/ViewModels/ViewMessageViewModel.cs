using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.ViewModels
{
    public class ViewMessageViewModel
    {
        public Guid OriginMessageId { get; set; }
        public Guid OriginMessageSenderId { get; set; }
        public string OriginMessageSenderName { get; set; }
        public Guid UserListingId { get; set; }
        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}
