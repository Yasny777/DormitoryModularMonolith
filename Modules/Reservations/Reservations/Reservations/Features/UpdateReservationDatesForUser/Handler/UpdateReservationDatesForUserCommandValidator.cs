using FluentValidation;
using Reservations.Reservations.Features.UpdateReservationDatesForUser.Endpoint;

namespace Reservations.Reservations.Features.UpdateReservationDatesForUser.Handler;

public class UpdateReservationDatesForUserCommandValidator : AbstractValidator<UpdateReservationDatesForUserCommand>
{
    public UpdateReservationDatesForUserCommandValidator()
    {
        RuleFor(x => x.NewStartDate)
            .LessThan(x => x.NewEndDate)
            .WithMessage("Start date must be before end date.");

        RuleFor(x => x.NewStartDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Start date cannot be in the past.");
    }
}