using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.ViewModels
{
    public class LandListingViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title required. Please enter a title.")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Details required. Please enter some details about your listing.")]
        [MaxLength(2000)]
        public string Details { get; set; }
        [Required(ErrorMessage = "Location required.")]
        public string Location { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(3)]
        public string Country { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        [Precision(7, 2)]
        [Required(ErrorMessage = "Price required. Please enter a number")]
        public decimal Price { get; set; }
        public DateTimeOffset AvailableDate { get; set; }
        [MaxLength(25)]
        public string LandType { get; set; }
        [MaxLength(25)]
        public string SiteFoundation { get; set; }
        [MaxLength(25)]
        public string DrivewayFoundation { get; set; }
        public bool WifiConnection { get; set; }
        public bool WaterConnection { get; set; }
        public bool ElectricalConnection { get; set; }
        public bool Parking { get; set; }
        public bool ChildFriendly { get; set; }
        public bool PetFriendly { get; set; }
        public bool NoSmoking { get; set; }
        public bool Private { get; set; }
        public string? Status { get; set; }
        public bool Approved { get; set; }
        public bool Submitted { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? MainImage { get; set; }

        public Guid ListerId { get; set; }
        public Guid UserListingId { get; set; }
    }
}
