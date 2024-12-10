namespace Dormitories.Dormitories.Features.UpdateRoom.Handler;

public record UpdateRoomCommand(Guid DormitoryId, Guid RoomId, string Number, int Capacity, decimal Price) : ICommand<UpdateRoomResult>;