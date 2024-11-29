using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.AddOcuupantToRoom.Handler;

public record AddOccupantToRoomCommand(Guid RoomId, Guid UserId) : ICommand<AddOccupantToRoomResult>;