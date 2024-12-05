using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservationsByRoomId.Handler;

public record CancelReservationsByRoomIdCommand(Guid RoomId) : ICommand<CancelReservationsByRoomIdResult>;