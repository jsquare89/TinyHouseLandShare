using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class LandListing: Listing
    {
        public string MapLocation { get; set; }
        public decimal Price { get; set; }
        public string PayPeriod { get; set; }
        public DateTimeOffset AvailableDate { get; set; }
        public string LotSize { get; set; }
        public string LandType { get; set; }
        public string FoundationSize { get; set; }
        public string SiteFoundation { get; set; }
        public string DrivewayFoundation { get; set; }
        public bool WifiConnection { get; set; }
        public bool WaterConnection { get; set; }
        public bool ElectricalConnection { get; set; }
        public bool Parking { get; set; }
        public bool ChildFriendly { get; set; }
        public bool Pets { get; set; }
        public bool SmokingPermitted { get; set; }
        public string Privacy { get; set; }

    }
}
