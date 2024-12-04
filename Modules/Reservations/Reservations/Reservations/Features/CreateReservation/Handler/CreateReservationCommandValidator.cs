using FluentValidation;
using Reservations.Reservations.Features.CreateReservation.Endpoint;

namespace Reservations.Reservations.Features.CreateReservation.Handler;

internal class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.RoomId).NotEmpty().WithMessage("RoomId is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}