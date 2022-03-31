using System;
using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserListingId { get; set; }
        public Guid SenderId { get; set; }
        //public Guid ReceiverId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        [MaxLength(500)]
        public string Value { get; set; }
        public bool IsViewed { get; set; }
        public Guid? OriginMessageId { get; set; } = null;



        [Required]
        public UserEntity Sender { get; set; }
        public UserListing UserListing { get; set; }
        public Message OriginMessage { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
