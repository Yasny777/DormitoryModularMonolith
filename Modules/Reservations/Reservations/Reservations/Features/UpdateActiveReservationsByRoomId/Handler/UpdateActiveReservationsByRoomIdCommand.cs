using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.UpdateActiveReservationsByRoomId.Handler;

public record UpdateActiveReservationsByRoomIdCommand(Guid Id, string Number, int Capacity, decimal Price)
    : ICommand<UpdateActiveReservationsByRoomIdResult>
{
}