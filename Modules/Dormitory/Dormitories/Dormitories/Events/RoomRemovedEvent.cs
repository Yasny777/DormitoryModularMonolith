using Dormitories.Dormitories.Models;

namespace Dormitories.Dormitories.Events;

public record RoomRemovedEvent(Room Room) : IDomainEvent
{

}