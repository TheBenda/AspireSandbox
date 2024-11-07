using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class OrderToppingItemConfiguration : IEntityTypeConfiguration<OrderToppingItem>
{
    public void Configure(EntityTypeBuilder<OrderToppingItem> entity)
    {
        entity.HasOne(d => d.OrderItem)
            .WithMany(o => o.OrderToppingItems)
            .HasForeignKey(fk => fk.OrderItemId);
        
        entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        entity.Property(e => e.Price).HasColumnType("money").IsRequired();
        entity.HasIndex(e => e.ToppingId, "IX_OrderToppingItem_ToppingId");
    }
}