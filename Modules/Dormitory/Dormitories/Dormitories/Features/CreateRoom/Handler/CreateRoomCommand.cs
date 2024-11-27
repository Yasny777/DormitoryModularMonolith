using Dormitory.Contracts.Dto;
using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.CreateRoom.Handler;

public record CreateRoomCommand(Guid DormitoryId, RoomDto RoomDto) : ICommand<CreateRoomResult>;