using Reservations.Reservations.Dto;

namespace Reservations.Reservations.Features.GetUserReservation.Handler;

internal record GetUserReservationResult(List<ReservationDto> Reservations)
{
}