using App.Domain.Core.Entities;
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

        public IList<string> Services { get; set; } = new List<string>();
    }
}
