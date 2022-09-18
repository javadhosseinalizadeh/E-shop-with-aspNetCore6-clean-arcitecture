namespace App.Domain.Core.Entities
{
    public partial class OrderFile
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int OrderId { get; set; }


        public virtual AppFile File { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
