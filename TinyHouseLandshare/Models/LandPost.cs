namespace TinyHouseLandshare.Models
{
    public class LandPost: Post
    {
        public string MapLocation { get; set; }
        public string Price { get; set; }
        public DateTimeOffset AvailableDate { get; set; }
        public string LotSize { get; set; }
        public string FoundationSize { get; set; }
        public string SiteFoundation { get; set; }
        public string DrivewayFoundation { get; set; }
        public string WifiConnection { get; set; }
        public string WaterConnection { get; set; }
        public string ElectricalConnection { get; set; }
        public string Parking { get; set; }
        public string ChildFriendly { get; set; }
        public string Pets { get; set; }
        public string SmokingPermitted { get; set; }
        public string Privacy { get; set; }

    }
}
