using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly LandShareDbContext _context;

        public MessagingService(LandShareDbContext context)
        {
            _context = context;
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

        public int GetUnreadMessagesCount(Guid userId)
        {
            return GetMessages(userId).Where(message => message.IsViewed.Equals(false)).Count();
        }

        public Message SendMessage(Message message)
        {
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
    }
}
