using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class LandListing: Listing
    {
        [MaxLength(100)]
        public string MapLocation { get; set; }
        [Precision(7,2)]
        public decimal Price { get; set; }
        [MaxLength(25)]
        public string PayPeriod { get; set; }
        public DateTimeOffset AvailableDate { get; set; }
        [MaxLength(25)]
        public string LotSize { get; set; }
        [MaxLength(25)]
        public string LandType { get; set; }
        [MaxLength(25)]
        public string FoundationSize { get; set; }
        [MaxLength(25)]
        public string SiteFoundation { get; set; }
        [MaxLength(25)]
        public string DrivewayFoundation { get; set; }
        public bool WifiConnection { get; set; }
        public bool WaterConnection { get; set; }
        public bool ElectricalConnection { get; set; }
        public bool Parking { get; set; }
        public bool ChildFriendly { get; set; }
        public bool Pets { get; set; }
        public bool SmokingPermitted { get; set; }
        [MaxLength(25)]
        public string Privacy { get; set; }


        public UserListing UserListing { get; set; }

    }
}
