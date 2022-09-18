using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class BidDto
    {
        [Display(Name = ("شناسه"))]
        public int Id { get; set; }

        [Display(Name = ("شناسه سفارش"))]
        public int OrderId { get; set; }

        [Display(Name = ("شناسه متخصص"))]
        public int ExpertId { get; set; }
        public string? ExpertName { get; set; }

        [Display(Name = ("قیمت پیشنهادی"))]
        public int SuggestedPrice { get; set; }

        [Display(Name = ("توضیحات"))]
        public string? Description { get; set; }

        [Display(Name = ("وضعیت تایید"))]
        public bool? IsConfirmedByCustomer { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public DateTimeOffset CreationDate { get; set; }

        [Display(Name = ("تاریخ ثبت"))]
        public string? ShamsiCreationDate { get; set; }

    }
}
