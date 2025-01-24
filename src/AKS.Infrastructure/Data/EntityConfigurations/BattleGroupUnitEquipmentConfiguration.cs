using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class BattleGroupUnitEquipmentConfiguration : IEntityTypeConfiguration<BattleGroupUnitEquipment>
{
    public void Configure(EntityTypeBuilder<BattleGroupUnitEquipment> entity)
    {
        entity.HasOne(d => d.BattleGroupUnit)
            .WithMany(o => o.BattleGroupUnitEquipments)
            .HasForeignKey(fk => fk.BattleGroupUnitId);
        
        entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        entity.Property(e => e.Points).IsRequired();
        entity.HasIndex(e => e.EquipmentId, "IX_BattleGroupUnitEquipment_EquipmentId");
    }
}