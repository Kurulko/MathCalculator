using System.ComponentModel.DataAnnotations;

namespace MyCalculator.Models.ViewModels
{
    public class RegisterModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter your name!")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter your email address!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
