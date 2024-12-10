namespace Dormitories.Dormitories.Features.DeleteRoom.Handler;

internal record DeleteRoomCommand(Guid DormitoryId, Guid RoomId) : ICommand;