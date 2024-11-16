using Dormitories.Dormitories.Models;

namespace Dormitories.Dormitories.Events;

public record RoomCreatedEvent(Room Room) : IDomainEvent;