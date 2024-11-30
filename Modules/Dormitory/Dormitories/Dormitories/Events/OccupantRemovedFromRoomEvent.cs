namespace Dormitories.Dormitories.Events;

public record OccupantRemovedFromRoomEvent(Guid RoomId, string UserId) : IDomainEvent;
