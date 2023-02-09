using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Application.API.Models
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryImage> CategoryImage { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductOwner> ProductOwner { get; set; }
        public virtual DbSet<WishlistItem> WishlistItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Application;Data Source=DESKTOP-NAR20KV\\SQLEXPRESS");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<CategoryImage>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryImage)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__CategoryI__Categ__267ABA7A");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.Modifiedby).HasMaxLength(200);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Product__Categor__2B3F6F97");

                entity.HasOne(d => d.ProductOwner)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductOwnerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Product__Product__2C3393D0");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImage)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ProductIm__Produ__2F10007B");
            });

            modelBuilder.Entity<ProductOwner>(entity =>
            {
                entity.Property(e => e.OwnerAdobject)
                    .HasColumnName("OwnerADObject")
                    .HasMaxLength(200);

                entity.Property(e => e.OwnerName).HasMaxLength(1000);
            });

            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.Property(e => e.OwnerAdobject)
                    .HasColumnName("OwnerADObject")
                    .HasMaxLength(200);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WishlistItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__WishlistI__Produ__31EC6D26");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
