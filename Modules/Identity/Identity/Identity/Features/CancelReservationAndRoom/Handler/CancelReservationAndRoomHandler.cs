using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.CancelReservationAndRoom.Handler;

public class
    CancelReservationAndRoomHandler : ICommandHandler<CancelReservationAndRoomCommand, CancelReservationAndRoomResult>
{
    public async Task<CancelReservationAndRoomResult> Handle(CancelReservationAndRoomCommand request,
        CancellationToken cancellationToken)
    {

    }
}

public record CancelReservationAndRoomResult(bool IsSuccess)
{
}

public record CancelReservationAndRoomCommand(Guid RoomId) : ICommand<CancelReservationAndRoomResult>
{
}