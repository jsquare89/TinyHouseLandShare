using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
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
