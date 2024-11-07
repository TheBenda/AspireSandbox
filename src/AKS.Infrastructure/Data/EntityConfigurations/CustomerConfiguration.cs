using AKS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKS.Infrastructure.Data.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.OwnsOne(o => o.Address, address =>
        {
            address.WithOwner();

            address.Property(a => a.ZipCode)
                .HasMaxLength(18)
                .IsRequired();

            address.Property(a => a.Street)
                .HasMaxLength(180)
                .IsRequired();

            address.Property(a => a.State)
                .HasMaxLength(60);

            address.Property(a => a.Country)
                .HasMaxLength(90)
                .IsRequired();

            address.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();
        });
        
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<UuiDv7Generator>();
        entity.Property(e => e.FirstName)
            .IsRequired();
        entity.Property(e => e.LastName)
            .IsRequired();
    }
}