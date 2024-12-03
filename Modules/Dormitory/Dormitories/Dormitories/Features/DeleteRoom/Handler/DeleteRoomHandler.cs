using Dormitories.Data.Repository;
using Dormitories.Dormitories.Features.DeleteRoom.Endpoint;
using Shared.Contracts.CQRS;
using Shared.Exceptions;

namespace Dormitories.Dormitories.Features.DeleteRoom.Handler;

internal class DeleteRoomHandler(IDormitoryRepository repository)
    : ICommandHandler<DeleteRoomCommand>
{
    public async Task<Unit> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var dormitory = await repository.GetDormitoryById(command.DormitoryId, cancellationToken);

        if (dormitory == null)
            throw new NotFoundException($"Dormitory with ID {command.DormitoryId} not found.");

        dormitory.RemoveRoom(command.RoomId);

        await repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}