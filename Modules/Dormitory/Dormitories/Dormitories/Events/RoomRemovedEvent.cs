namespace Dormitories.Dormitories.Events;

public record RoomRemovedEvent(Room Room) : IDomainEvent
{

}