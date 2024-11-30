using Microsoft.Extensions.Caching.Distributed;
using Reservations.Reservations.Features.CancelReservation.Endpoint;
using Reservations.Reservations.Services;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservation.Handler;

internal class CancelReservationHandler(ReservationDbContext reservationDbContext, IRedisService redisService)
    : ICommandHandler<CancelReservationCommand, CancelReservationResult>
{
    public async Task<CancelReservationResult> Handle(CancelReservationCommand request,
        CancellationToken cancellationToken)
    {
        using var transaction = await reservationDbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var reservation =
                await reservationDbContext.Reservations.FirstOrDefaultAsync(r => r.Id == request.ReservationId,
                    cancellationToken);

            if (reservation == null) throw new NotFoundException("Reservation", request.ReservationId);

            reservation.Cancel();

            await reservationDbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            await redisService.DecrementOccupantsAsync(reservation.RoomId, cancellationToken);

            return new CancelReservationResult(true);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}