using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class SeekerListing: Listing
    {
        [MaxLength(25)]
        public string HouseSize { get; set; }
        [MaxLength(25)]
        public string PreferredLandType { get; set; }
        public int OccupantCount { get; set; }
        public bool InternetConnectionRequired { get; set; }
        public bool WaterConnectionRequired { get; set; }
        public bool ElectricalConnectionRequired { get; set; }
        public bool ParkingRequired { get; set; }
        public bool ChildFriendlyRequired { get; set; }
        public bool PetFriendlyRequired { get; set; }


        public UserListing UserListing { get; set; }
    }
}
