namespace Dormitories.Dormitories.Features.UpdateRoom.Handler;

internal class UpdateRoomHandler(IDormitoryRepository repository)
    : ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
{
    public async Task<UpdateRoomResult> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        var dormitory = await repository.GetDormitoryById(command.DormitoryId, cancellationToken);

        if (dormitory == null)
            throw new NotFoundException($"Dormitory with ID {command.DormitoryId} not found.");

        var room = dormitory.UpdateRoom(command.RoomId, command.Number, command.Capacity, command.Price);

        await repository.SaveChangesAsync(cancellationToken);

        var roomDto = room.Adapt<RoomDto>();
        return new UpdateRoomResult(roomDto);
    }
}