using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class BidDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ExpertUserId { get; set; }
        public int SuggestedPrice { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
