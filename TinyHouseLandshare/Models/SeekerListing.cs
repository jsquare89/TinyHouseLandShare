namespace TinyHouseLandshare.Models
{
    public class SeekerListing: Listing
    {
        public string HouseSize { get; set; }
        public int OccupantCount { get; set; }
        public bool WifiConnectionRequired { get; set; }
        public bool WaterConnectionRequired { get; set; }
        public bool ElectricalConnectionRequired { get; set; }
        public bool ParkingRequired { get; set; }
        public bool ChildFriendlyRequired { get; set; }
        public bool PetsRequired { get; set; }
        public bool Smoker { get; set; }
        public bool Privacy { get; set; }
    }
}
