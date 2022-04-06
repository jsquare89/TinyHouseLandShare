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
                    where ul.UserId == userId

                    select new HeadMessageViewModel
                    {
                        Id = ul.Id,
                        SeekerOrLandListingId = (Guid)(ul.SeekerListingId != null ? ul.SeekerListingId : ul.LandListingId),
                        TimeStamp = m.TimeStamp,
                        SenderId = m.SenderId,
                        SenderName = u.Name,
                        IsViewed = m.IsViewed,
                        Value = m.Value
                    }).ToList();
            results = UpdateMessageHeadTitles(results);
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

        public Message SendMessage(Guid senderId,
                                   Guid listingId,
                                   string messageValue)
        {
            var message = new Message
            {
                SenderId = senderId,
                UserListingId = listingId,
                TimeStamp = DateTimeOffset.UtcNow,
                Value = messageValue,
                IsViewed = false
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
    }
}
