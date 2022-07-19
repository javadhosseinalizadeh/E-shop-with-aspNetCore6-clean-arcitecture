namespace App.Domain.Core.Entities
{
    public partial class AppFile
    {
        public AppFile()
        {
            OrderFiles = new HashSet<OrderFile>();
            ServiceFiles = new HashSet<ServiceFile>();
        }

        public int Id { get; set; }
        public int EntityId { get; set; }
        public string FileAddress { get; set; } = null!;
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Entity Entity { get; set; } = null!;
        public virtual ICollection<OrderFile> OrderFiles { get; set; }
        public virtual ICollection<ServiceFile> ServiceFiles { get; set; }
    }
}
