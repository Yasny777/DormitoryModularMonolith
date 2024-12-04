using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetUserReservation.Handler;

internal record GetUserReservationQuery(string UserId) : IQuery<GetUserReservationResult>;