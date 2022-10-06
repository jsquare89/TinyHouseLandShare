using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.ViewModels
{
    public class SeekerListingViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Details required.")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Location required.")]
        public string Location { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }

        [MaxLength(25)]
        public string? HouseSize { get; set; }
        public int OccupantCount { get; set; }
        public bool InternetConnectionRequired { get; set; }
        public bool WaterConnectionRequired { get; set; }
        public bool ElectricalConnectionRequired { get; set; }
        [MaxLength(25)]
        public string? PreferedLandType { get; set; }
        public bool ParkingRequired { get; set; }
        public bool ChildFriendlyRequired { get; set; }
        public bool PetsRequired { get; set; }


        [MaxLength(50)]
        public string Status { get; set; }
        public bool Approved { get; set; }
        public bool Submitted { get; set; }

        public Guid ListerId { get; set; }
        public Guid UserListingId { get; set; }
    }
}
