namespace Dormitories.Dormitories.Events;

public record OccupantRemovedFromRoomEvent(Guid RoomId, Guid UserId) : IDomainEvent;
