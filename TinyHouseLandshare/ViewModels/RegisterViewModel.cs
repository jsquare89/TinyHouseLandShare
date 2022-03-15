using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match. ")]
        public string ConfirmPassword { get; set; }
    }
}
