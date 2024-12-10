namespace Dormitories.Dormitories.Events;

public record RoomCreatedEvent(Room Room) : IDomainEvent;