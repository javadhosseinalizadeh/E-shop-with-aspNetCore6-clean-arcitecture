using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.InfraStructures.Database.SqlServer.Data
{
    public partial class AppDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Bid> Bids { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<UserFile> UserFiles { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ExpertFavoriteCategory> ExpertFavoriteCategories { get; set; } = null!;
        public virtual DbSet<AppFile> Files { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderFile> OrderFiles { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceComment> ServiceComments { get; set; } = null!;
        public virtual DbSet<ServiceFile> ServiceFiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>()
                .Property(x => x.Title)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceComment>()
                .Property(x => x.Title)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceComment>()
                .Property(x => x.Description)
                .HasMaxLength(2000);

            modelBuilder.Entity<OrderStatus>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<AppFile>()
                .Property(x => x.Path)
                .HasMaxLength(500);

            #region Order

            modelBuilder.Entity<Order>()
                .Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(2000);

            modelBuilder.Entity<Order>()
                .Property(x => x.FinalPrice)
                .IsRequired(false);

            //--------------------------------------------------
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Service)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ServiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceComment>()
                .HasOne(x => x.Order)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bid>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.Bids)
                .HasForeignKey(x => x.ExpertId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Order>()
                .HasOne(x => x.Status)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            //------------------------------------------------------------------------
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.CustomerOrders)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.ExpertOrders)
                .HasForeignKey(x => x.ConfirmedExpertId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            //--------------------------------------------------------------------------

            #endregion

            modelBuilder.Entity<ExpertFavoriteCategory>()
                .HasOne(x => x.Expert)
                .WithMany(x => x.ExpertFavoriteCategories)
                .HasForeignKey(x => x.ExpertUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpertFavoriteCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.ExpertFavoriteCategories)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bid>()
                .HasOne(x => x.Order)
                .WithMany(x => x.ExpertSuggests)
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Service>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ServiceFile>()
                .HasOne(x => x.Service)
                .WithMany(x => x.ServiceFiles)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.ServiceFiles)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderFile>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderFiles)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.OrderFiles)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserFile>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserFiles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserFile>()
                .HasOne(x => x.File)
                .WithMany(x => x.UserFiles)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Seed();

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
