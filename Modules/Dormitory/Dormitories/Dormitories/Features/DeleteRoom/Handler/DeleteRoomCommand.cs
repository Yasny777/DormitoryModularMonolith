using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.DeleteRoom.Handler;

public record DeleteRoomCommand(Guid DormitoryId, Guid RoomId) : ICommand;