using FluentValidation;

namespace Dormitories.Dormitories.Features.UpdateRoom.Handler;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.DormitoryId)
            .NotEmpty()
            .WithMessage("Dormitory ID cannot be empty.");

        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage("Room ID cannot be empty.");

        RuleFor(x => x.Number)
            .NotEmpty()
            .MaximumLength(5)
            .WithMessage("Room number cannot exceed 5 characters.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0)
            .WithMessage("Capacity must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a positive value.");
    }
}