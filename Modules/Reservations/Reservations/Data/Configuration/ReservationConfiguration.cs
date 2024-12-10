using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reservations.Data.Configuration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(r => r.Status)
            .HasConversion(
                status => status.Value, // Z encji do bazy (string)
                value => ReservationStatus.FromValue(value)) // Z bazy do encji (ReservationStatus)
            .IsRequired().HasMaxLength(50);

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.RoomId).IsRequired();

        builder.Property(r => r.StartDate).IsRequired();

        builder.Property(r => r.EndDate).IsRequired();

        builder.ComplexProperty(r => r.RoomInfo, roomInfoBuilder =>
        {
            roomInfoBuilder.Property(x => x.Capacity).IsRequired();
            roomInfoBuilder.Property(x => x.Number).IsRequired().HasMaxLength(5);
            roomInfoBuilder.Property(x => x.Price).IsRequired().HasPrecision(18, 2);
        });


    }
}