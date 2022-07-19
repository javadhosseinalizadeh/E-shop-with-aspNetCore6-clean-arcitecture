namespace App.Domain.Core.Entities
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public byte Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
