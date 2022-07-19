namespace App.Domain.Core.Entities
{
    public partial class Category
    {
        public Category()
        {
            ExpertFavoriteCategories = new HashSet<ExpertFavoriteCategory>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<ExpertFavoriteCategory> ExpertFavoriteCategories { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
