using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class ServiceCommentDto
    {
        [Display(Name = ("شناسه"))]
        public int Id { get; set; }

        [Display(Name = ("شناسه سفارش"))]
        public int OrderId { get; set; }

        [Display(Name = ("شناسه سرویس"))]
        public int? ServiceId { get; set; }

        [Display(Name = ("تیتر"))]
        public string Title { get; set; }

        [Display(Name = ("توضیحات"))]
        public string Description { get; set; }

        [Display(Name = ("ثبت توسط"))]
        public bool IsWriteByCustomer { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public DateTimeOffset CreationDate { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public string? ShamsiCreationDate { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = ("وضعیت تایید"))]
        public bool? IsApproved { get; set; }
    }
}
