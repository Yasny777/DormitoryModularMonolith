using Reservations.Reservations.Dto;

namespace Reservations.Reservations.Features.GetUserReservation.Handler;

public record GetUserReservationResult(List<ReservationDto> ReservationDto)
{
}