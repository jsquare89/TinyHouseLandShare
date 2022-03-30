using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyHouseLandshare.Models
{
    public class UserEntity: IdentityUser<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }



        [ForeignKey("UserId")]
        public ICollection<UserListing> UserListings { get; set; }
        [ForeignKey("SenderId")]
        public ICollection<Message> Messages { get; set; }
    }
}
