using Dormitory.Dormitories.Events;

namespace Dormitory.Dormitories.Models;

public class Room : Entity<Guid>
{
    public Guid DormitoryId { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public int Capacity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    //todo User type will come from User Module (reference guid to Users module)
     private readonly List<Guid> _occupants = [];
     public IReadOnlyList<Guid> Occupants => _occupants.AsReadOnly();
     public int TotalOccupants => Occupants.Count;

     public bool IsFull() => TotalOccupants >= Capacity;
    internal Room(Guid dormitoryId, string number, int capacity, decimal price)
    {
        DormitoryId = dormitoryId;
        Number = number;
        Capacity = capacity;
        Price = price;
    }

    public IDomainEvent AddOccupant(Guid userId)
    {
        // logika
        //_occupants.Add()
        //return new OccupantAddedToRoomEvent(roomId, userId);
        throw new NotImplementedException();
    }

    public IDomainEvent RemoveOccupant(Guid userId)
    {
        throw new NotImplementedException();
    }
}