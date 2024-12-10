namespace Reservations.Reservations.Features.UpdateReservationDatesForUser.Handler;

public record UpdateReservationDatesForUserCommand(Guid ReservationId, DateTime NewStartDate, DateTime NewEndDate) : ICommand<UpdateReservationDatesForUserResult>;