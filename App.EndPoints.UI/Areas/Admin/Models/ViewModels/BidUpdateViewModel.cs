using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels
{
    public class BidUpdateViewModel
    {
        [Required]
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "شناسه سفارش")]
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "شناسه متخصص")]
        public int ExpertUserId { get; set; }
        [Required]
        [Display(Name = "قیمت پیشنهادی")]
        public int SuggestedPrice { get; set; }
        [Required]
        [Display(Name = "وضعیت تایید")]
        public bool IsApproved { get; set; }
        [Required]
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreatedAt { get; set; }
        public List<int> StatusIds { get; set; } = new List<int>();

    }
}
