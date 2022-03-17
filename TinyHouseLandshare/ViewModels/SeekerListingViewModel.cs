using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.ViewModels
{
    public class SeekerListingViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string Location { get; set; }

    }
}
