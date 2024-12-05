using Reservations.Reservations.Models;
using Reservations.Reservations.Services;
using Reservations.Reservations.ValueObjects;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservationsByRoomId;

public class CancelReservationsByRoomIdHandler(ReservationDbContext dbContext, IRedisService redisService)
    : ICommandHandler<CancelReservationsByRoomIdQuery, CancelReservationsByRoomIdResult>
{
    public async Task<CancelReservationsByRoomIdResult> Handle(CancelReservationsByRoomIdQuery request,
        CancellationToken cancellationToken)
    {
        var reservations =  await dbContext.Reservations.Where(r =>
            r.RoomId == request.RoomId && r.Status == ReservationStatus.Active).ToListAsync(cancellationToken);

        foreach (var reservation in reservations)
        {
            reservation.CancelReservation();
            dbContext.Reservations.Update(reservation);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        await redisService.ClearCacheAsync(request.RoomId, cancellationToken);

        //todo change return type
        return new CancelReservationsByRoomIdResult(true);
    }
}

//todo seed semester active