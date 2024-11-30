using Microsoft.Extensions.Caching.Distributed;
using Reservations.Reservations.Features.CancelReservation.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservation.Handler;

public class CancelReservationHandler(ReservationDbContext reservationDbContext, IDistributedCache redis)
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

            // after commit need to decrease in redis db too
            var redisKeyOccupants = $"room:{reservation.RoomId}:occupants";
            var roomOccupantsCount = await redis.GetStringAsync(redisKeyOccupants, cancellationToken);
            var newOccupantsCount = int.Parse(roomOccupantsCount);
            newOccupantsCount -= 1;
            await redis.SetStringAsync(redisKeyOccupants, newOccupantsCount.ToString(), cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return new CancelReservationResult(true);
    }
}