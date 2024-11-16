namespace Dormitories.Dormitories.Models;

public class Dormitory : Aggregate<Guid>
{
    public string Name { get; private set; } = default!;
    public string Category { get; private set; } = default!;
    public string ContactEmail { get; private set; } = default!;
    public string ContactNumber { get; private set; } = default!;
    public Address Address { get; private set; } = default!;

    private readonly List<Room> _rooms = [];
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();


    public static Dormitory Create(Guid id, string name, string category, string contactEmail, string contactNumber, Address address)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(category);

        var dormitory = new Dormitory()
        {
            Id = id,
            Name = name,
            Category = category,
            ContactEmail = contactEmail,
            ContactNumber = contactNumber,
            Address = address
        };
        // adddomainevent dormitorycreated
        return dormitory;
    }


    public void AddRoom(string number, int capacity, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var room = _rooms.FirstOrDefault(x => x.Number == number);

        if (room != null)
        {
            throw new Exception("nie ma"); //todo custom exception
        }

        var newRoom = new Room(Id, number, capacity, price);
        _rooms.Add(newRoom);
    }

    public void AddOccupantToRoom(Guid roomId, Guid userId)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId)
                   ?? throw new InvalidOperationException("Room not found.");

        // Room generuje zdarzenie
        var occupantAddedToRoomEvent = room.AddOccupant(userId);

        // Root agregat rejestruje zdarzenie
        AddDomainEvent(occupantAddedToRoomEvent);
    }

    public void RemoveOccupantFromRoom(Guid roomId, Guid userId)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId)
                   ?? throw new InvalidOperationException("Room not found.");

        // Room generuje zdarzenie
        var occupantRemovedFromRoomEvent = room.RemoveOccupant(userId);

        // Root agregat rejestruje zdarzenie
        AddDomainEvent(occupantRemovedFromRoomEvent);
    }
}