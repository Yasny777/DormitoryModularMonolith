﻿namespace Dormitories.Dormitories.Events;

public record OccupantAddedToRoomEvent(Guid RoomId, Guid UserId) : IDomainEvent;