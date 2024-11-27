using Shared.Contracts.CQRS;

namespace Dormitories.Contracts.Dormitories.GetRoomById;

public record GetRoomByIdQuery(Guid RoomId) : IQuery<GetRoomByIdResult>
{
}