using Mapster;
using Reservations.Reservations.Dto;
using Reservations.Reservations.Features.GetUserReservationsById.Endpoint;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetUserReservationsById.Handler;

internal class GetUserReservationsByIdHandler(ReservationDbContext dbContext)
    : IQueryHandler<GetUserReservationsByIdQuery, GetUserReservationsByIdResult>
{
    public async Task<GetUserReservationsByIdResult> Handle(GetUserReservationsByIdQuery query, CancellationToken cancellationToken)
    {
        var reservations = await dbContext.Reservations
            .Include(r => r.Semester)
            .AsNoTracking()
            .Where(r => r.UserId == query.UserId)
            .ToListAsync(cancellationToken);

        if (reservations.Count == 0)
            throw new NotFoundException($"No reservations found for user with ID {query.UserId}");

        var reservationDtos = reservations.Adapt<List<ReservationDto>>();

        return new GetUserReservationsByIdResult(reservationDtos);
    }
}