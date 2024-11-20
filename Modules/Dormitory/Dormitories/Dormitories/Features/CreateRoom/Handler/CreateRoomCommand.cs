namespace Dormitories.Dormitories.Features.CreateRoom.Handler;

public record CreateRoomCommand(Guid DormitoryId, RoomDto RoomDto) : ICommand<CreateRoomResult>;