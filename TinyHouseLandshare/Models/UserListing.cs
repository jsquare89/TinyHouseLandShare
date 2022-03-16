using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class UserListing
    {
        public Guid User { get; set; }
        public Guid Listing { get; set; }
    }
}
