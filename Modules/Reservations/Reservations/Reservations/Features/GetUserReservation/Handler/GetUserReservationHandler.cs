using Mapster;
using Reservations.Reservations.Dto;
using Reservations.Reservations.Features.GetUserReservation.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetUserReservation.Handler;

public class GetUserReservationHandler(ReservationDbContext dbContext)
    : IQueryHandler<GetUserReservationQuery, GetUserReservationResult>
{
    public async Task<GetUserReservationResult> Handle(GetUserReservationQuery query,
        CancellationToken cancellationToken)
    {
        var reservation = await dbContext.Reservations.AsNoTracking()
            .SingleOrDefaultAsync(r => r.UserId == Guid.Parse(query.UserId), cancellationToken: cancellationToken);

        if (reservation == null) throw new NotFoundException("Reservation for user not found");

        var reservationDto = reservation.Adapt<ReservationDto>();

        return new GetUserReservationResult(reservationDto);
    }
}