using Dormitories.Data.Repository;
using Dormitories.Dormitories.Exceptions;
using Dormitories.Dormitories.Features.CreateRoom.Endpoint;
using Dormitories.Dormitories.Models;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Features.CreateRoom.Handler;
// todo add validators
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

            // room is reference to Room entity which is tracked and Id is assigned after savechanges
        return new CreateRoomResult(room.Id);
    }
}