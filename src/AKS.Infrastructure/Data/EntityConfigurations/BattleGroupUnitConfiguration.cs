using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class BattleGroupUnitConfiguration : IEntityTypeConfiguration<BattleGroupUnit>
{
    public void Configure(EntityTypeBuilder<BattleGroupUnit> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        entity.Property(e => e.Points).IsRequired();
        entity.Property(e => e.Health).IsRequired();
        entity.Property(e => e.Attack).IsRequired();
        entity.Property(e => e.Defense).IsRequired();
        entity.Property(e => e.Movement).IsRequired();
        entity.Property(e => e.Range).IsRequired();
        entity.Property(e => e.Accuracy).IsRequired();
        entity.Property(e => e.Name).IsRequired();
        entity.HasIndex(e => e.UnitId, "IX_BattleGroupUnit_UnitId");
        
        entity.HasOne(d => d.BattleGroup)
            .WithMany(o => o.BattleGroupUnits)
            .HasForeignKey(fk => fk.BattleGroupId);
    }
}