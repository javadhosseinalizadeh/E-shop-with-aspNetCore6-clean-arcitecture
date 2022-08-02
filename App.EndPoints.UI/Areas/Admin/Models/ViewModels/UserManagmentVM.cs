using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels
{
    public class UserManagmentVM
    {
        [Display(Name = "شناسه کاربری")]
        public int Id { get; init; }

        [Display(Name = "نام")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "رمز عبور")]
        public string Password { get; set; } = null!;

        [Display(Name = "ایمیل")]
        public string Email { get; set; } = null!;

        [Display(Name = "شماره تلفن")]
        public string? PhoneNumber { get; set; }
        public string? SecurityStamp { get; set; } = Guid.NewGuid().ToString();

        public IList<string> Roles { get; set; } = new List<string>();
    }
}
