using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservation.Handler;

internal record CancelReservationCommand(string UserId, Guid ReservationId) : ICommand<CancelReservationResult>;