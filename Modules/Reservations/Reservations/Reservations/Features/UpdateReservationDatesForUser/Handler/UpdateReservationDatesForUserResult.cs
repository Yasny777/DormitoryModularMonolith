namespace Reservations.Reservations.Features.UpdateReservationDatesForUser.Handler;

internal record UpdateReservationDatesForUserResult(Guid ReservationId, DateTime StartDate, DateTime EndDate);