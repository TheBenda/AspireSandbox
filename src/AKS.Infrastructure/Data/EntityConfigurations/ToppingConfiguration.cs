using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class ToppingConfiguration : IEntityTypeConfiguration<Topping>
{
    public void Configure(EntityTypeBuilder<Topping> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        entity.Property(e => e.Price).HasColumnType("money").IsRequired();
        entity.Property(e => e.Name).IsRequired();
            
        entity.HasIndex(e => e.ProductId, "IX_Products_ProductId");

        entity.HasOne(d => d.Product)
            .WithMany(o => o.Toppings)
            .HasForeignKey(fk => fk.ProductId);
    }
}