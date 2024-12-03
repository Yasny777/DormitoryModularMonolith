using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Reservations.Models;

namespace Reservations.Data.Configuration;

public class SemesterConfiguration : IEntityTypeConfiguration<Semester>
{
    public void Configure(EntityTypeBuilder<Semester> builder)
    {
        builder.HasKey(s => s.Id);

        // Mapowanie pól i ograniczenia
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100); // Ograniczenie długości nazwy semestru

        builder.Property(s => s.Number)
            .IsRequired();

        builder.Property(s => s.StartDate)
            .IsRequired();

        builder.Property(s => s.EndDate)
            .IsRequired();

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.HasMany(s => s.Reservations)
            .WithOne(r => r.Semester)
            .HasForeignKey(r => r.SemesterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}