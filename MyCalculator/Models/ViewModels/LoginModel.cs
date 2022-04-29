using System.ComponentModel.DataAnnotations;

namespace MyCalculator.Models.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter your name!")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
