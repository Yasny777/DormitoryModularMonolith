using Shared.Contracts.CQRS;

namespace Reservations.Reservations.Features.UpdateReservationDatesForUser.Handler;

internal record UpdateReservationDatesForUserCommand(Guid ReservationId, DateTime NewStartDate, DateTime NewEndDate) : ICommand<UpdateReservationDatesForUserResult>;