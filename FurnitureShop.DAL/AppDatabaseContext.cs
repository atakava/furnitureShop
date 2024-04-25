using FurnitureShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL;

public class AppDatabaseContext : DbContext
{
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<ProductParameter> ProductParameters { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<SliderItem> SliderItems { get; set; }
    public DbSet<Banner> Banners { get; set; }

    public DbSet<Admin> Admins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=Test;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductParameter>()
            .HasKey(pp => new { pp.ParameterId, pp.ProductId });

        modelBuilder.Entity<ProductParameter>()
            .HasOne(pp => pp.Product)
            .WithMany(p => p.ProductParameters)
            .HasForeignKey(pp => pp.ProductId);

        modelBuilder.Entity<ProductParameter>()
            .HasOne(pp => pp.Parameter)
            .WithMany()
            .HasForeignKey(pp => pp.ParameterId);

        modelBuilder.Entity<Catalog>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Catalog)
            .HasForeignKey(p => p.CatalogId);
    }
}