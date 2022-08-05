using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Areas.Expert.Models.ViewModels
{
    public class ExpertRegisterViewModel
    {
        [Required]
        [Display(Name = "نام")]
        public string FirstName { get; set; } = null!;
        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = null!;
        [Required]
        [Display(Name = "آدرس")]
        public string HomeAddress { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "{0} حداقل {2} کاراکتر و حداکتر {1} باشد", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار رمز هبور باید یکسان باشند")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
