using Dormitories.Dormitories.Models;

namespace Dormitories.Dormitories.Events;

public record RoomUpdatedEvent(Room Room) : IDomainEvent
{
    
}