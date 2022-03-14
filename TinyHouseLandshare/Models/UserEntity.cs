using Microsoft.AspNetCore.Identity;
using System;

namespace TinyHouseLandshare.Models
{
    public class UserEntity: IdentityUser<Guid>
    {
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
