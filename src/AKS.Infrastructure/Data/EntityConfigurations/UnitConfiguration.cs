using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.Property(e => e.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        builder.Property(e => e.Points).IsRequired();
        builder.Property(e => e.Health).IsRequired();
        builder.Property(e => e.Attack).IsRequired();
        builder.Property(e => e.Defense).IsRequired();
        builder.Property(e => e.Movement).IsRequired();
        builder.Property(e => e.Range).IsRequired();
        builder.Property(e => e.Accuracy).IsRequired();
        builder.Property(e => e.Name).IsRequired();
    }
}