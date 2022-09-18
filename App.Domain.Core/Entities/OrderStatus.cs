namespace App.Domain.Core.Entities
{
    public partial class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StatusValue { get; set; }
        public DateTimeOffset CreationDate { get; set; }



        public virtual List<Order> Orders { get; set; }
    }
}
