using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        entity.Property(e => e.Price).HasColumnType("money").IsRequired();
        entity.HasIndex(e => e.ProductId, "IX_OrderItem_ProductId");
        
        entity.HasOne(d => d.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(fk => fk.OrderId);
    }
}