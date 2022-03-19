﻿using TinyHouseLandshare.Data;
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
            return _context.Messages.Where(message => message.SenderId == userId);
        }

        public int GetMessagesCount(Guid userId)
        {
            return GetMessages(userId).Count();
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
