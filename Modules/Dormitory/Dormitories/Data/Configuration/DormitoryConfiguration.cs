using Dormitories.Dormitories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dormitories.Data.Configuration;

public class DormitoryConfiguration : IEntityTypeConfiguration<Dormitory>
{
    public void Configure(EntityTypeBuilder<Dormitory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

        builder.Property(x => x.Category).HasMaxLength(50).IsRequired();

        builder.Property(x => x.ContactNumber).HasMaxLength(15);

        builder.Property(x => x.ContactEmail).HasMaxLength(254);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Rooms).WithOne().HasForeignKey(x => x.DormitoryId);

        builder.ComplexProperty(d => d.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.Street).IsRequired().HasMaxLength(150);
            addressBuilder.Property(x => x.City).IsRequired().HasMaxLength(50);
            addressBuilder.Property(x => x.ZipCode).IsRequired().HasMaxLength(5);
        });

    }
}