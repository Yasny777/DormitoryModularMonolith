using Dormitories.Dormitories.Exceptions;

namespace Dormitories.Dormitories.Features.CreateRoom.Handler;
internal class CreateRoomHandler(IDormitoryRepository repository)
    : ICommandHandler<CreateRoomCommand, CreateRoomResult>
{
    public async Task<CreateRoomResult> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var dormitory = await repository.GetDormitoryById(request.DormitoryId, cancellationToken);
        if (dormitory == null) throw new DormitoryNotFoundException(request.DormitoryId);

        var room = dormitory.AddRoom(
            request.RoomDto.Number,
            request.RoomDto.Category,
            request.RoomDto.Capacity,
            request.RoomDto.Price);

        await repository.SaveChangesAsync(cancellationToken);

        return new CreateRoomResult(room.Id);
    }
}
