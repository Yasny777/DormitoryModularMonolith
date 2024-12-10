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


    // Dormitory management
    public static Dormitory Create(Guid id, string name, string category, string contactEmail, string contactNumber,
        Address address)
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

    public void Update(string name, string category, Address address)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;

        if (!string.IsNullOrWhiteSpace(category))
            Category = category;

        Address = address;
    }

    // Room management
    public Room AddRoom(string number, string category, int capacity, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentException.ThrowIfNullOrEmpty(category);

        var room = _rooms.FirstOrDefault(x => x.Number == number);

        if (room != null)
        {
            throw new NotFoundException($"Room with that number {room.Number} already exists");
        }

        var newRoom = new Room(Id, number, capacity, category, price);
        _rooms.Add(newRoom);

        // can add domain event here

        return newRoom;
    }

    public void RemoveRoom(Guid roomId)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId);
        if (room == null)
            throw new InvalidOperationException($"Room with ID {roomId} does not exist in dormitory {Id}.");

        _rooms.Remove(room);

        AddDomainEvent(new RoomRemovedEvent(room));
    }

    public Room UpdateRoom(Guid roomId, string number, int capacity, decimal price)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId)
                   ?? throw new InvalidOperationException($"Room with ID {roomId} not found in dormitory {Id}.");

        if (room.Number != number && _rooms.Any(r => r.Number == number))
            throw new InvalidOperationException($"Room with that number {number} already exists");

        room.UpdateDetails(number, capacity, price);

        AddDomainEvent(new RoomUpdatedEvent(room));

        return room;
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

    public void RemoveOccupantFromRoom(Guid roomId, string userId)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId)
                   ?? throw new InvalidOperationException("Room not found.");

        // Room generuje zdarzenie
        var occupantRemovedFromRoomEvent = room.RemoveOccupant(userId);

        // Root agregat rejestruje zdarzenie
        AddDomainEvent(occupantRemovedFromRoomEvent);
    }
}