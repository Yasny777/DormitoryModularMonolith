using Mapster;
using Reservations.Reservations.Dto;
using Reservations.Reservations.Features.GetUserReservation.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetUserReservation.Handler;

internal class GetUserReservationHandler(ReservationDbContext dbContext)
    : IQueryHandler<GetUserReservationQuery, GetUserReservationResult>
{
    public async Task<GetUserReservationResult> Handle(GetUserReservationQuery query,
        CancellationToken cancellationToken)
    {
        var reservations = await dbContext.Reservations
            .Include(r => r.Semester)
            .AsNoTracking()
            .Where(r => r.UserId == Guid.Parse(query.UserId))
            .ToListAsync(cancellationToken);

        if (reservations == null || reservations.Count == 0)
            throw new NotFoundException("Reservation for user not found");

        var reservationDto = reservations.Adapt<List<ReservationDto>>();

        return new GetUserReservationResult(reservationDto);
    }
}