namespace Dormitories.Dormitories.ValueObjects;

public class RoomOccupant
{
    public Guid RoomId { get; set; }
    public string AppUserId { get; set; } = default!; // Użytkownik jako string bo IdentityManager mapuje Guid na string
}