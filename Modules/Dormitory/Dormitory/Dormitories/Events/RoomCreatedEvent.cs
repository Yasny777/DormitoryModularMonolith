using Dormitory.Dormitories.Models;

namespace Dormitory.Dormitories.Events;

public record RoomCreatedEvent(Room Room) : IDomainEvent;