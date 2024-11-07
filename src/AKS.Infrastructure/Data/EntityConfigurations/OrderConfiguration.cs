using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.Metadata.FindNavigation(nameof(Order.OrderItems))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<UuiDv7Generator>();
            
        entity.Property(e => e.OrderPlaced)
            .IsRequired();
            
        entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

        entity.HasOne(d => d.Customer)
            .WithMany(o => o.Orders)
            .HasForeignKey(fk => fk.CustomerId);
        
        entity.OwnsOne(o => o.ShipmentAddress, shipmentAddress =>
        {
            shipmentAddress.WithOwner();

            shipmentAddress.Property(a => a.ZipCode)
                .HasMaxLength(18)
                .IsRequired();

            shipmentAddress.Property(a => a.Street)
                .HasMaxLength(180)
                .IsRequired();

            shipmentAddress.Property(a => a.State)
                .HasMaxLength(60);

            shipmentAddress.Property(a => a.Country)
                .HasMaxLength(90)
                .IsRequired();

            shipmentAddress.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();
        });
        
        entity.Navigation(e => e.ShipmentAddress).IsRequired();
    }
}