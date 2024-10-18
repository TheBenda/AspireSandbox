using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Data;

public class PrimaryDbContext(DbContextOptions<PrimaryDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public DbSet<Topping> Toppings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.HasOne(d => d.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(fk => fk.CustomerId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");


            entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("double precision");
        });

        modelBuilder.Entity<Topping>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Products_ProductId");

            entity.HasOne(d => d.Product)
                .WithMany(o => o.Toppings)
                .HasForeignKey(fk => fk.ProductId);
        });
    }
    // dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    // dotnet add package Microsoft.EntityFrameworkCore.Tools    
    // Scaffold: dotnet aspnet-codegenerator minimalapi -e CustomerApi -m Customer -dc AKS.Infrastructure.Data.PrimaryDbContext --relativeFolderPath Apis --databaseProvider postgres --open
    // dotnet aspnet-codegenerator minimalapi -e ProductApi -m Product -dc POS.Persistence.Data.PizzaOrderDbContext --relativeFolderPath Apis --databaseProvider postgres --open  
}
