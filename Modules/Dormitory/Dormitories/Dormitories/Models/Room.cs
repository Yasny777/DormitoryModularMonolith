using Dormitories.Dormitories.Events;

namespace Dormitories.Dormitories.Models;

public class Room : Entity<Guid>
{
    public Guid DormitoryId { get; private set; } = default!;
    public string Number { get; private set; } = default!;

    public string Category { get; private set; } = default!;

    public int Capacity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    //todo User type will come from User Module (reference guid to Users module)
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


    public IDomainEvent AddOccupant(Guid userId)
    {
        _occupants.Add(new RoomOccupant
        {
            RoomId = Id,
            AppUserId = userId.ToString()
        });

        return new OccupantAddedToRoomEvent(Id, userId);

    }

    public IDomainEvent RemoveOccupant(Guid userId)
    {
        throw new NotImplementedException();
    }
}