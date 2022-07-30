using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels
{
    public class ServiceCommentVM
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "نام خدمات")]
        public int ServiceId { get; set; }
        [Display(Name = "شناسه درخواست")]
        public int OrderId { get; set; }
        [Display(Name = "متن نظر")]
        public string? CommentText { get; set; }
        [Display(Name = "شناسه مشتری")]
        public int CreatedUserId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreatedAt { get; set; }
    }
}
