using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Models
{
    public class RegisterViewModel
    {


        [DataType(DataType.Text)]
        [Display(Name = ("نام کاربری"))]
        [Required(ErrorMessage = ("وارد کردن نام کاربری الزامیست"))]
        [Remote("EnsureUsernameIsNotExist", "Account", ErrorMessage = "این نام کاربری قابل انتخاب نمی باشد")]
        public string UserName { get; set; }

        [Display(Name = ("ایمیل"))]
        [Required(ErrorMessage = ("وارد کردن ایمیل الزامیست"))]
        [DataType(DataType.EmailAddress)]
        [Remote("EnsureEmailIsNotExist", "Account", ErrorMessage = "این ایمیل قابل انتخاب نمی باشد")]
        public string Email { get; set; }

        [Display(Name = ("رمز عبور"))]
        [Required(ErrorMessage = "رمز عبور حداقل 3 حرف باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = ("تکرار رمز عبور"))]
        [Required(ErrorMessage = "تکرار رمز عبور حداقل 3 حرف باشد")]
        [DataType(DataType.Password)]

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور متفاوت هستند")]
        public string ConfirmPassword { get; set; }


        [Display(Name = ("نام"))]
        [Required(ErrorMessage = ("وارد کردن نام الزامیست"))]
        public string? FirstName { get; set; }

        [Display(Name = ("نام خانوادگی"))]
        [Required(ErrorMessage = ("وارد کردن نام خانوادگی الزامیست"))]
        public string? LastName { get; set; }
    }
}
