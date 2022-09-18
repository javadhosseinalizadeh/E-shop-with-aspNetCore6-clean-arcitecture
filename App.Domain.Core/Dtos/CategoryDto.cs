using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class CategoryDto
    {
        [Display(Name = ("شناسه"))]
        public int Id { get; set; }

        [Display(Name = ("نام دسته بندی"))]
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        public string Title { get; set; }

        public List<ServiceDto> Services { get; set; }
    }
}
