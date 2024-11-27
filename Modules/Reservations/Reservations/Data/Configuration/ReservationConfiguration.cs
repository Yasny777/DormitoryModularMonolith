using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Reservations.Models;
using Reservations.Reservations.ValueObjects;

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
    }
}