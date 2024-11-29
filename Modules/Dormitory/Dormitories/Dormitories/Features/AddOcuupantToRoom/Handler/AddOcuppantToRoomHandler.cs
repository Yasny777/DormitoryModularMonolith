using Dormitories.Data.Repository;
using Dormitories.Dormitories.EventHandlers;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Features.AddOcuupantToRoom.Handler;

internal class AddOcuppantToRoomHandler(IDormitoryRepository dormitoryRepository)
    : ICommandHandler<AddOccupantToRoomCommand, AddOccupantToRoomResult>
{
    public async Task<AddOccupantToRoomResult> Handle(AddOccupantToRoomCommand request,
        CancellationToken cancellationToken)
    {
        var dormitory = await dormitoryRepository.GetDormitoryByRoomId(request.RoomId, cancellationToken);

        if (dormitory == null) throw new NotFoundException("Dormitory not found");

        dormitory.AddOccupantToRoom(request.RoomId, request.UserId);

        await dormitoryRepository.SaveChangesAsync(cancellationToken);

        return new AddOccupantToRoomResult(true);
    }
}