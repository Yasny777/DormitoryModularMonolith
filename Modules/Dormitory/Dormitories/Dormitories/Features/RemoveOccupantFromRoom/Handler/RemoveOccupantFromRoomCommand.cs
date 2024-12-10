namespace Dormitories.Dormitories.Features.RemoveOccupantFromRoom.Handler;

internal record RemoveOccupantFromRoomCommand(Guid RoomId, Guid UserId) : ICommand<RemoveOccupantFromRoomResult>;