namespace App.Domain.Core.Entities
{
    public partial class AppFile
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public virtual List<UserFile> UserFiles { get; set; }
        public virtual List<ServiceFile> ServiceFiles { get; set; }
        public virtual List<OrderFile> OrderFiles { get; set; }
    }
}
