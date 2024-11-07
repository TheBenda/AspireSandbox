using System.Reflection;

using AKS.Domain.Entities;
using AKS.Domain.Values;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace AKS.Infrastructure.Data;

public class PrimaryDbContext(DbContextOptions<PrimaryDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    //public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public DbSet<Topping> Toppings { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<OrderToppingItem> OrderToppingItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEntities(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Alternative way to configure Entities.
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Customer>(entity =>
        // {
        //     entity.Property(e => e.Id)
        //         .ValueGeneratedOnAdd()
        //         .HasValueGenerator<UuiDv7Generator>();
        //     entity.Property(e => e.FirstName)
        //         .IsRequired();
        //     entity.Property(e => e.LastName)
        //         .IsRequired();
        // });
        //
        // modelBuilder.Entity<Order>(entity =>
        // {
        //     entity.Property(e => e.Id)
        //         .ValueGeneratedOnAdd()
        //         .HasValueGenerator<UuiDv7Generator>();
        //     
        //     entity.Property(e => e.OrderPlaced)
        //         .IsRequired();
        //     
        //     entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");
        //
        //     entity.HasOne(d => d.Customer)
        //         .WithMany(o => o.Orders)
        //         .HasForeignKey(fk => fk.CustomerId);
        // });
        //
        // modelBuilder.Entity<Product>(entity =>
        // {
        //     entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        //     entity.Property(e => e.Price).HasColumnType("money").IsRequired();
        //     entity.Property(e => e.Name).IsRequired();
        // });
        //
        // modelBuilder.Entity<Topping>(entity =>
        // {
        //     entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        //     entity.Property(e => e.Price).HasColumnType("money").IsRequired();
        //     entity.Property(e => e.Name).IsRequired();
        //     
        //     entity.HasIndex(e => e.ProductId, "IX_Products_ProductId");
        //
        //     entity.HasOne(d => d.Product)
        //         .WithMany(o => o.Toppings)
        //         .HasForeignKey(fk => fk.ProductId);
        // });
    }


    // dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    // dotnet add package Microsoft.EntityFrameworkCore.Tools    
    // Scaffold: dotnet aspnet-codegenerator minimalapi -e CustomerApi -m Customer -dc AKS.Infrastructure.Data.PrimaryDbContext --relativeFolderPath Apis --databaseProvider postgres --open
    // dotnet aspnet-codegenerator minimalapi -e ProductApi -m Product -dc POS.Persistence.Data.PizzaOrderDbContext --relativeFolderPath Apis --databaseProvider postgres --open  
}


