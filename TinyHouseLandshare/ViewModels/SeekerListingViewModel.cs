using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.ViewModels
{
    public class SeekerListingViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title required.")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Details required.")]
        [MaxLength(2000)]
        public string Details { get; set; }
        [Required(ErrorMessage = "Location required.")]
        [MaxLength(25)]
        public string Location { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(2)]
        public string Country { get; set; }

        [MaxLength(25)]
        public string HouseSize { get; set; }
        [MaxLength(25)]
        public string PreferredLandType { get; set; }
        public int OccupantCount { get; set; }
        public bool InternetConnectionRequired { get; set; }
        public bool WaterConnectionRequired { get; set; }
        public bool ElectricalConnectionRequired { get; set; }
        public bool ChildFriendlyRequired { get; set; }
        public bool PetFriendlyRequired { get; set; }

        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public bool Approved { get; set; }
        public bool Submitted { get; set; }

        public string? ImageSrc { get; set; }
        public IFormFile? MainImage { get; set; }


        public Guid ListerId { get; set; }
        public Guid UserListingId { get; set; }
    }
}
