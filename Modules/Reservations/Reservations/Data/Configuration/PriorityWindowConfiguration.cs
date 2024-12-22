using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reservations.Data.Configuration;


public class PriorityWindowConfiguration : IEntityTypeConfiguration<PriorityWindow>
{
    public void Configure(EntityTypeBuilder<PriorityWindow> builder)
    {
        builder.HasKey(pw => pw.Id);

        builder.HasOne(pw => pw.Semester)
            .WithOne(s => s.PriorityWindow)
            .HasForeignKey<PriorityWindow>(pw => pw.SemesterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(pw => pw.StartDateTime)
            .IsRequired();

        builder.Property(pw => pw.EndDateTime)
            .IsRequired();

        builder.Property(pw => pw.RoleNames)
            .HasConversion(
                v => string.Join(",", v), 
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .IsRequired();

    }
}