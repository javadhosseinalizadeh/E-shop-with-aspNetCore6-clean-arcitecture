using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class AppFileDto
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string FileAddress { get; set; } = null!;
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
