using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.ViewModels
{
    public class LandListingViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Details required.")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Location required.")]
        public string Location { get; set; }
        public string State { get; set; }
        [MaxLength(3)]
        public string Country { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        [MaxLength(100)]
        public string MapLocation { get; set; }
        [Precision(7, 2)]
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



        public Guid ListerId { get; set; }
        public Guid UserListingId { get; set; }
    }
}
