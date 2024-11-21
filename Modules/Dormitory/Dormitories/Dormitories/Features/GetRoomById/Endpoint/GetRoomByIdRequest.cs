namespace Dormitories.Dormitories.Features.GetRoomById.Endpoint;

public record GetRoomByIdRequest(Guid DormitoryId, Guid RoomId)
{
}