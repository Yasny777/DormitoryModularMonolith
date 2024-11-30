using Dormitories.Dormitories.Events;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Models;

public class Room : Entity<Guid>
{
    public Guid DormitoryId { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public string Category { get; private set; } = default!;
    public int Capacity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    private readonly List<RoomOccupant> _occupants = new();
    public IReadOnlyList<RoomOccupant> Occupants => _occupants.AsReadOnly();
    public int TotalOccupants => Occupants.Count;
    public bool IsAvailable => TotalOccupants < Capacity;

    internal Room(Guid dormitoryId, string number, int capacity, string category, decimal price)
    {
        DormitoryId = dormitoryId;
        Number = number;
        Capacity = capacity;
        Category = category;
        Price = price;
    }


    public OccupantAddedToRoomEvent AddOccupant(Guid userId)
    {
        _occupants.Add(new RoomOccupant
        {
            RoomId = Id,
            AppUserId = userId.ToString()
        });

        return new OccupantAddedToRoomEvent(Id, userId);
    }

    public OccupantRemovedFromRoomEvent RemoveOccupant(string userId)
    {
        var occupantToRemove = _occupants.Find(o => o.AppUserId == userId);
        if (occupantToRemove == null) throw new NotFoundException("Occupant not found in the room");
        _occupants.Remove(occupantToRemove);
        return new OccupantRemovedFromRoomEvent(Id, userId);
    }
}