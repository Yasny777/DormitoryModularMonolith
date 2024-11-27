using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CreateReservation.Handler;

public record CreateReservationCommand(Guid RoomId, Guid UserId) : ICommand<CreateReservationResult>;