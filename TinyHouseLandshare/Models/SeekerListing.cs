﻿using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class SeekerListing: Listing
    {
        [MaxLength(25)]
        public string HouseSize { get; set; }
        public int OccupantCount { get; set; }
        public bool WifiConnectionRequired { get; set; }
        public bool WaterConnectionRequired { get; set; }
        public bool ElectricalConnectionRequired { get; set; }
        [MaxLength(25)]
        public string PreferedLandType { get; set; }
        public bool ParkingRequired { get; set; }
        public bool ChildFriendlyRequired { get; set; }
        public bool PetsRequired { get; set; }
        public bool Smoker { get; set; }
        [MaxLength(25)]
        public string Privacy { get; set; }


        public UserListing UserListing { get; set; }
    }
}
