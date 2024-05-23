using Microsoft.EntityFrameworkCore;
using Storage = StorageApp.Model.Storage;

namespace StorageApp.DB
{
    public class StorageContext : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Port=5433;Username=admin;Password=Storage1234;Database=StorageDB");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("storage_pkey");

                entity.ToTable("storages");

                entity.Property(e => e.Id).HasColumnName("id");
            });
        }
    }
}
