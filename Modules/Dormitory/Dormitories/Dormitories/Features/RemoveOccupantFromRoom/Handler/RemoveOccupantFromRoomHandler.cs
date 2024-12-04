using Dormitories.Data.Repository;
using Dormitories.Dormitories.EventHandlers;
using Dormitories.Dormitories.Features.AddOcuupantToRoom.Handler;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Features.RemoveOccupantFromRoom.Handler;

internal class RemoveOccupantFromRoomHandler(IDormitoryRepository dormitoryRepository) : ICommandHandler<RemoveOccupantFromRoomCommand, RemoveOccupantFromRoomResult>
{
    public async Task<RemoveOccupantFromRoomResult> Handle(RemoveOccupantFromRoomCommand request, CancellationToken cancellationToken)
    {
        var dormitory = await dormitoryRepository.GetDormitoryByRoomId(request.RoomId, cancellationToken);

        if (dormitory == null) throw new NotFoundException("Dormitory not found");

        dormitory.RemoveOccupantFromRoom(request.RoomId, request.UserId.ToString());

        await dormitoryRepository.SaveChangesAsync(cancellationToken);

        return new RemoveOccupantFromRoomResult(true);
    }
}