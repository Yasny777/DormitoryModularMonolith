using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetUserReservation.Handler;

public record GetUserReservationQuery(string UserId) : IQuery<GetUserReservationResult>;