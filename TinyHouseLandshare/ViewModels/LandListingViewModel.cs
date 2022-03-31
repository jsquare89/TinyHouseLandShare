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
    }
}
