namespace App.Domain.Core.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public int? ConfirmedExpertId { get; set; }
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public int? FinalPrice { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Service Service { get; set; }
        public virtual AppUser Customer { get; set; }
        public virtual AppUser Expert { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual List<ServiceComment> Comments { get; set; }
        public virtual List<Bid> ExpertSuggests { get; set; }
        public virtual List<OrderFile> OrderFiles { get; set; }
    }
}
