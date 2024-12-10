using Reservations.Reservations.Services;
using Reservations.Reservations.ValueObjects;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservationsByRoomId.Handler;

public class CancelReservationsByRoomIdHandler(ReservationDbContext dbContext, IRedisService redisService)
    : ICommandHandler<CancelReservationsByRoomIdCommand, CancelReservationsByRoomIdResult>
{
    public async Task<CancelReservationsByRoomIdResult> Handle(CancelReservationsByRoomIdCommand request,
        CancellationToken cancellationToken)
    {
        var reservations = await dbContext
            .Reservations
            .Where(r => r.RoomId == request.RoomId && r.Status == ReservationStatus.Active)
            .ToListAsync(cancellationToken);

        foreach (var reservation in reservations)
        {
            reservation.CancelReservation();
            dbContext.Reservations.Update(reservation);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        await redisService.ClearCacheAsync(request.RoomId, cancellationToken);

        return new CancelReservationsByRoomIdResult(true);
    }
}

//todo seed semester active, better seeding to do