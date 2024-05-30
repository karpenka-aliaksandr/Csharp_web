using Microsoft.EntityFrameworkCore;
using ProductInStorageApp.Model;

namespace ProductInStorageApp.DB
{
    public class ProductInStorageContext : DbContext
    {
        private string _connectionString;
        public ProductInStorageContext()
        {
        }
        public ProductInStorageContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<ProductInStorage> ProductInStorages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInStorage>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("product_pkey");

                entity.ToTable("storages");

                entity.Property(e => e.StorageId).HasColumnName("storage_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Price).HasColumnName("price");
            });
        }
    }
}
