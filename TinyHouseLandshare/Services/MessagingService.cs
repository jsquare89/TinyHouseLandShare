using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly LandShareDbContext _context;
        private readonly IUserListingRepository _userListingRepository;

        public MessagingService(LandShareDbContext context,
                                IUserListingRepository userListingRepository)
        {
            _context = context;
            _userListingRepository = userListingRepository;
        }
        public IEnumerable<Message> GetMessages(Guid userId)
        {
            var results =  from m in _context.Messages
                           join ul in _context.UserListings on m.UserListingId equals ul.Id
                           where ul.UserId == userId
                           orderby m.TimeStamp descending
                           select m;
            return results.ToList();                           
        }

        public int GetMessagesCount(Guid userId)
        {
            return GetMessages(userId).Count();
        }

        public IEnumerable<Message> GetUserMessageHeads(Guid userId)
        {
            return GetMessages(userId).GroupBy(message => message.UserListingId, 
                (key, group) => group.OrderByDescending(item => item.TimeStamp).First());
                
        }

        public IEnumerable<HeadMessageViewModel> GetUserMessageHeadsAsViewModels(Guid userId)
        {
            List<HeadMessageViewModel> headMessageVMs = GetMessageHeadsFromContextForUser(userId);
            return headMessageVMs;
        }

        private List<HeadMessageViewModel> GetMessageHeadsFromContextForUser(Guid userId)
        {
            var results = (from m in _context.Messages
                    join ul in _context.UserListings
                      on m.UserListingId equals ul.Id
                    join u in _context.Users
                      on m.SenderId equals u.Id
                    where m.ReceiverId == userId || m.SenderId == userId
                    orderby m.TimeStamp descending

                    select new HeadMessageViewModel
                    {
                        OriginMessageId = (Guid)(m.OriginMessageId == null? m.Id : m.OriginMessageId),
                        MessageId = m.Id,
                        SeekerOrLandListingId = (Guid)(ul.SeekerListingId != null ? ul.SeekerListingId : ul.LandListingId),
                        TimeStamp = m.TimeStamp,
                        SenderId = m.SenderId,
                        SenderName = u.Name,
                        IsViewed = m.IsViewed,
                        Value = m.Value
                    }).ToList();
            results = UpdateMessageHeadTitles(results);
            results = results.OrderByDescending(m => m.TimeStamp).GroupBy(m => m.OriginMessageId).Select(m => m.FirstOrDefault()).ToList();
            return results;
        }

        private List<HeadMessageViewModel> UpdateMessageHeadTitles(List<HeadMessageViewModel> results)
        {
            foreach (var item in results)
            {
                item.Title = _userListingRepository.GetListingTitleBySeekerOrLandListingId(item.SeekerOrLandListingId);
            }
            return results;
        }

        public int GetUnreadMessagesCount(Guid userId)
        {
            return GetMessages(userId).Where(message => message.IsViewed.Equals(false)).Count();
        }

        public Message SendInitialdMessage(Guid senderId, 
                                           Guid receiverId,
                                           Guid userListingId,
                                           string messageValue)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                UserListingId = userListingId,
                TimeStamp = DateTimeOffset.UtcNow,
                Value = messageValue,
                IsViewed = false,
            };

            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public Message SendReplyMessage(Guid senderId, 
                                        Guid receiverId,
                                        Guid userListingId,
                                        string messageValue,
                                        Guid originMessageId)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                UserListingId = userListingId,
                TimeStamp = DateTimeOffset.UtcNow,
                Value = messageValue,
                IsViewed = false,
                OriginMessageId = originMessageId
            };

            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public Message SetMessageViewed(Guid messageId)
        {
            var message = _context.Messages.Find(messageId);
            message.IsViewed = true;
            _context.Update(message);
            _context.SaveChanges();
            return message;
        }

        public IEnumerable<Message> GetMessagesByHeadId(Guid headId)
        {
            var results = _context.Messages.
                Where(m => m.Id.Equals(headId) || m.OriginMessageId.Equals(headId)).
                OrderBy(m => m.TimeStamp);
            return results;
        }

        public (Guid id, string name) GetOriginMessageSender(Guid messageId)
        {
            var senderId = _context.Messages.Find(messageId).SenderId;
            var senderName = _context.Users.Find(senderId).Name;
            return (senderId, senderName);
        }

        public Guid GetMessageUserListingId(Guid messageId)
        {
            var userListingId = _context.Messages.Find(messageId).UserListingId;
            return userListingId;
        }


    }
}
