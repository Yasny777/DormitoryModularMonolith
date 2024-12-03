using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.GetUserReservationsById.Handler;

public record GetUserReservationsByIdQuery(Guid UserId) : IQuery<GetUserReservationsByIdResult>;