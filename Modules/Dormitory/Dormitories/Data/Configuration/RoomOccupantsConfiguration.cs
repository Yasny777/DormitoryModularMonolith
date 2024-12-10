namespace Dormitories.Data.Configuration;

public class RoomOccupantsConfiguration : IEntityTypeConfiguration<RoomOccupant>
{
    public void Configure(EntityTypeBuilder<RoomOccupant> modelBuilder)
    {
        modelBuilder
            .HasKey(ro => new { ro.RoomId, ro.AppUserId }); // Klucz złożony

        modelBuilder
            .HasOne<Room>() // Nawigacja do Room
            .WithMany(r => r.Occupants)
            .HasForeignKey(ro => ro.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}