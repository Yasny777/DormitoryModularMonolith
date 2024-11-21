namespace Dormitories.Dormitories.Features.GetRoomById.Handler;

public record GetRoomByIdQuery(Guid DormitoryId, Guid RoomId) : IQuery<GetRoomByIdResult>
{
}