namespace App.Domain.Core.Entities
{
    public partial class Bid
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ExpertUserId { get; set; }
        public int SuggestedPrice { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
