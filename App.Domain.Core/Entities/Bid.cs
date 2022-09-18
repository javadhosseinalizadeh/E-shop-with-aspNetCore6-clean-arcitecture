namespace App.Domain.Core.Entities
{
    public partial class Bid
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ExpertId { get; set; }
        public int SuggestedPrice { get; set; }
        public string? Description { get; set; }
        public bool? IsConfirmedByCustomer { get; set; }
        public DateTimeOffset CreationDate { get; set; }


        public virtual Order Order { get; set; }
        public virtual AppUser Expert { get; set; }
    }
}
