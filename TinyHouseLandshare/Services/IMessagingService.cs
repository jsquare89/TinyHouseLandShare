using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Services
{
    public interface IMessagingService
    {
        Message SendMessage(Guid senderId,
                            Guid listingId,
                            string messageValue);
        IEnumerable<Message> GetMessages(Guid userId);
        IEnumerable<Message> GetUserMessageHeads(Guid userId);
        int GetMessagesCount(Guid userId);
        int GetUnreadMessagesCount(Guid userId);
        Message SetMessageViewed(Guid messageId);
    }
}
