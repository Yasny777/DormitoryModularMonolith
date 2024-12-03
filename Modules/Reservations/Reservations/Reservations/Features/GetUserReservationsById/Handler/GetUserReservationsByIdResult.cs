using Reservations.Reservations.Dto;

namespace Reservations.Reservations.Features.GetUserReservationsById.Handler;

public record GetUserReservationsByIdResult(List<ReservationDto> Reservations);