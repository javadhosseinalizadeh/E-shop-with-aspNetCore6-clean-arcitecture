namespace App.Domain.Core.Entities
{
    public partial class ExpertFavoriteCategory
    {
        public int Id { get; set; }
        public int ExpertUserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
