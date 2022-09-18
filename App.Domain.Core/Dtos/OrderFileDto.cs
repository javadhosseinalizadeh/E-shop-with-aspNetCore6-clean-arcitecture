using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class OrderFileDto
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int OrderId { get; set; }
    }
}
