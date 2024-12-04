using Dormitories.Contracts.Dto;
using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.CreateRoom.Handler;

internal record CreateRoomCommand(Guid DormitoryId, RoomDto RoomDto) : ICommand<CreateRoomResult>;