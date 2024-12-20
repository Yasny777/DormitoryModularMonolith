namespace Dormitories.Dormitories.Events;

public record RoomAddedEvent(Room Room) : IDomainEvent;