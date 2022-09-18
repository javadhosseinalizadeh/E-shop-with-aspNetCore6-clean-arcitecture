namespace App.Domain.Core.Entities
{
    public partial class ServiceComment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public bool IsWriteByCustomer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsApproved { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public virtual Order Order { get; set; }
    }
}
