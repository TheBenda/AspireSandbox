using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        entity.Property(e => e.Points).IsRequired();
        entity.Property(e => e.Name).IsRequired();
            
        entity.HasIndex(e => e.UnitId, "IX_Units_UnitId");

        entity.HasOne(d => d.Unit)
            .WithMany(o => o.Equipments)
            .HasForeignKey(fk => fk.UnitId);
    }
}