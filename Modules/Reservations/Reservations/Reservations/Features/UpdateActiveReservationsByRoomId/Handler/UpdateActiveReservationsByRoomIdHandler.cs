using Reservations.Reservations.ValueObjects;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.UpdateActiveReservationsByRoomId.Handler;

public class UpdateActiveReservationsByRoomIdHandler(ReservationDbContext dbContext)
    : ICommandHandler<UpdateActiveReservationsByRoomIdCommand,
        UpdateActiveReservationsByRoomIdResult>
{
    public async Task<UpdateActiveReservationsByRoomIdResult> Handle(UpdateActiveReservationsByRoomIdCommand request,
        CancellationToken cancellationToken)
    {
        var reservations = await dbContext
            .Reservations
            .Where(r => r.RoomId == request.Id && r.Status == ReservationStatus.Active)
            .ToListAsync(cancellationToken);

        foreach (var reservation in reservations)
        {
            reservation.Update(request.Number, request.Capacity, request.Price);
            dbContext.Reservations.Update(reservation);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateActiveReservationsByRoomIdResult(true);
    }
}