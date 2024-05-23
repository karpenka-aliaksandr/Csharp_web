using Market.Models;
using Microsoft.EntityFrameworkCore;
using Storage = Market.Models.Storage;

namespace Market.DB
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Port=5432;Username=admin;Password=Market1234;Database=Market");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("product_pkey");

                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .HasColumnName("description");

                entity.HasOne(e => e.Group).WithMany(g => g.Products).HasForeignKey(e=>e.GroupId);
            });
            
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("group_pkey");

                entity.ToTable("groups");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("storage_pkey");

                entity.ToTable("storages");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<ProductStorage>(entity =>
            {
                entity.HasKey(e => new {e.ProductId,e.StorageId}).HasName("product_storage_pkey");
                
                entity.ToTable("product_storage");

                entity.HasOne(ps => ps.Storage).WithMany(s=>s.Products).HasForeignKey(ps=>ps.StorageId);
                entity.HasOne(ps => ps.Product).WithMany(s => s.Storages).HasForeignKey(ps => ps.ProductId);
            });
        }
    }
}
