using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservation.Handler;

public record CancelReservationCommand(string UserId, Guid ReservationId) : ICommand<CancelReservationResult>;