using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class OrderStatusDto
    {
        public byte Id { get; set; }
        public string Title { get; set; } = null!;
    }
}
