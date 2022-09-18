using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class OrderStatusDto
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTimeOffset CreationDate { get; set; }
        [Display(Name = ("تاریخ ثبت"))]
        public string? ShamsiCreationDate { get; set; }
    }
}
