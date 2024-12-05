using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservationsByRoomId.Handler;

public record CancelReservationsByRoomIdQuery(Guid RoomId) : ICommand<CancelReservationsByRoomIdResult>;