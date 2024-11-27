using Shared.Contracts.CQRS;

namespace Dormitory.Contracts.Dormitories.GetRoomById;

public record GetRoomByIdQuery(Guid RoomId) : IQuery<GetRoomByIdResult>
{
}