namespace Dormitory.Dormitories.Events;

public record OccupantRemovedFromRoomEvent(Guid RoomId, Guid UserId) : IDomainEvent;
