using Reservations.Reservations.Dto;

namespace Reservations.Reservations.Features.GetUserReservationsById.Handler;

internal record GetUserReservationsByIdResult(List<ReservationDto> Reservations);