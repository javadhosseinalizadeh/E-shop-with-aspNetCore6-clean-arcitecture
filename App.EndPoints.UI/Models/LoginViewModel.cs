using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; } = null!;

        [Display(Name = "من را به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
