using System.ComponentModel.DataAnnotations;
namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        [Display(Name = "وضعیت")]
        public int Id { get; set; }
        [Required]
        [Display(Name ="وضعیت")]
        public byte StatusId { get; set; }
        [Required]
        [Display(Name = "شناسه خدمات")]
        public int ServiceId { get; set; }
        [Required]
        [Display(Name = "قیمت پایه")]
        public int ServiceBasePrice { get; set; }
        [Required]
        [Display(Name = "شناسه مشتری")]
        public int? CustomerUserId { get; set; }
        [Required]
        [Display(Name = "قیمت نهایی")]
        public int? FinalExpertUserId { get; set; }
        [Required]
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreatedAt { get; set; }
    }
}
