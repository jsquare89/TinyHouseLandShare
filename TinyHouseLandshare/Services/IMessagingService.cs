using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Services
{
    public interface IMessagingService
    {
        Message SendInitialdMessage(Guid senderId,
                                    Guid receiverId,
                                    Guid listingId,
                                    string messageValue);
        Message SendReplyMessage(Guid senderId,
                                 Guid receiverId,
                                 Guid userListingId,
                                 string messageValue,
                                 Guid originMessageId);

        IEnumerable<Message> GetMessages(Guid userId);
        IEnumerable<Message> GetUserMessageHeads(Guid userId);
        IEnumerable<Message> GetMessagesByHeadId(Guid headId);
        IEnumerable<HeadMessageViewModel> GetUserMessageHeadsAsViewModels(Guid userId);
        int GetMessagesCount(Guid userId);
        int GetUnreadMessagesCount(Guid userId);
        Message SetMessageViewed(Guid messageId);

        (Guid id, string name) GetOriginMessageSender(Guid messageId);
        Guid GetMessageUserListingId(Guid messageId);
    }
}
