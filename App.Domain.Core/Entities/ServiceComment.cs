namespace App.Domain.Core.Entities
{
    public partial class ServiceComment
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int OrderId { get; set; }
        public string? CommentText { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
