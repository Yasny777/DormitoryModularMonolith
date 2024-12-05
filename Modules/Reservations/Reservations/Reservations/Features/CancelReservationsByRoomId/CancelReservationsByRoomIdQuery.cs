using Reservations.Reservations.Models;
using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.CancelReservationsByRoomId;

public record CancelReservationsByRoomIdQuery(Guid RoomId) : ICommand<CancelReservationsByRoomIdResult>;