using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.RemoveOccupantFromRoom.Handler;

public record RemoveOccupantFromRoomCommand(Guid RoomId, Guid UserId) : ICommand<RemoveOccupantFromRoomResult>;