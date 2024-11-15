namespace Dormitory.Dormitories.Models;

public class Dormitory : Aggregate<Guid>
{
    public string Name { get; private set; } = default!;
    public string Category { get; private set; } = default!;
    public string ContactEmail { get; private set; } = default!;
    public string ContactNumber { get; private set; } = default!;
    public Address Address { get; private set; } = default!;

    private readonly List<Room> _rooms = [];
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();

    public void AddRoom()
    {

    }
}