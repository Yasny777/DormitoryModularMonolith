namespace Reservations.Reservations.Features.UpdateReservationDatesForUser.Handler;

public record UpdateReservationDatesForUserResult(Guid ReservationId, DateTime StartDate, DateTime EndDate);