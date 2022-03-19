using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Services
{
    public interface IMessagingService
    {
        Message SendMessage(Message message);
        IEnumerable<Message> GetMessages(Guid userId);
        int GetMessagesCount(Guid userId);
        Message SetMessageViewed(Guid messageId);
    }
}
