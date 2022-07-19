namespace App.Domain.Core.Entities
{
    public partial class ServiceFile
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int ServiceId { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual AppFile File { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
