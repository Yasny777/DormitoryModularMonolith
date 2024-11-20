using Dormitories.Data.Repository;
using Dormitories.Dormitories.Features.CreateRoom.Endpoint;
using Dormitories.Dormitories.Models;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Features.CreateRoom.Handler;

internal class CreateRoomHandler(IDormitoryRepository repository)
    : ICommandHandler<CreateRoomCommand, CreateRoomResult>
{
    public async Task<CreateRoomResult> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var dormitory = await repository.GetDormitoryById(request.DormitoryId, cancellationToken);
        if (dormitory == null) throw new NotFoundException("Room", request.DormitoryId);

        var roomId = dormitory.AddRoom(
            request.RoomDto.Number,
            request.RoomDto.Category,
            request.RoomDto.Capacity,
            request.RoomDto.Price);

        await repository.SaveChangesAsync(cancellationToken);

        return new CreateRoomResult(roomId);
    }
}