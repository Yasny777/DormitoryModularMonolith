using Dormitories.Dormitories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dormitories.Data.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number).IsRequired().HasMaxLength(5);

        builder.Property(x => x.Category).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Capacity).IsRequired();
        builder.Property(x => x.Price).HasPrecision(18, 2);

        builder.HasIndex(x => x.Number).IsUnique();
    }
}