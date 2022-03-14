using Microsoft.AspNetCore.Identity;
using System;

namespace TinyHouseLandshare.Models
{
    public class UserRoleEntity: IdentityRole<Guid>
    {
        public UserRoleEntity(): base()
        {

        }

        public UserRoleEntity(string roleName)
            : base(roleName)
        {

        }
    }
}
