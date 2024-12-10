namespace Dormitories.Dormitories.Features.AddOcuupantToRoom.Handler;

internal record AddOccupantToRoomCommand(Guid RoomId, Guid UserId) : ICommand<AddOccupantToRoomResult>;