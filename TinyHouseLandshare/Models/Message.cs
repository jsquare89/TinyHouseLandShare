using System;

namespace TinyHouseLandshare.Models
{
    public class Message
    {
        public int Id { get; set; }
        public Guid LisitingId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Value { get; set; }
        public bool IsViewed { get; set; }
        public Guid ParentMessageId { get; set; }
    }
}
