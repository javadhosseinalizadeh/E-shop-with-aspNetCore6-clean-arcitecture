

using App.Domain.Core.Entities;

namespace App.Domain.Core.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public byte StatusId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceBasePrice { get; set; }
        public int? CustomerUserId { get; set; }
        public int? FinalExpertUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        //  public virtual OrderStatus Status { get; set; } = null!;
        public List<OrderStatusDto> Statuses { get; set; } = new List<OrderStatusDto>();


    }
}
