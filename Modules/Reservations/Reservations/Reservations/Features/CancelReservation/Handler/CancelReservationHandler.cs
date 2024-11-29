using Reservations.Reservations.Features.CancelReservation.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservation.Handler;

public class CancelReservationHandler(ReservationDbContext reservationDbContext)
    : ICommandHandler<CancelReservationCommand, CancelReservationResult>
{
    public async Task<CancelReservationResult> Handle(CancelReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation =
            await reservationDbContext.Reservations.FirstOrDefaultAsync(r => r.Id == request.ReservationId,
                cancellationToken);

        if (reservation == null) throw new NotFoundException("Reservation", request.ReservationId);

        reservation.Cancel();

        await reservationDbContext.SaveChangesAsync(cancellationToken);

        return new CancelReservationResult(true);
    }
}