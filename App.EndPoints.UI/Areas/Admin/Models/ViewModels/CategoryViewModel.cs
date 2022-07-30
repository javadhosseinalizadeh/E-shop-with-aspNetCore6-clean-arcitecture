using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "عنوان")]
        public string Title { get; set; } = null!;
    }
}
