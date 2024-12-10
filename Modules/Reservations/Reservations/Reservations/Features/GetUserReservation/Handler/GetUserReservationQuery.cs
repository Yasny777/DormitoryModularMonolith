namespace Reservations.Reservations.Features.GetUserReservation.Handler;

internal record GetUserReservationQuery(string UserId) : IQuery<GetUserReservationResult>;