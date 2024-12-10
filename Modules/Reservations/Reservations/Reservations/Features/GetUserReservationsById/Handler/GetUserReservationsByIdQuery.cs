namespace Reservations.Reservations.Features.GetUserReservationsById.Handler;

internal record GetUserReservationsByIdQuery(Guid UserId) : IQuery<GetUserReservationsByIdResult>;