using FluentValidation;

namespace Dormitories.Dormitories.Features.CreateRoom.Handler;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(command => command.DormitoryId)
            .NotEmpty().WithMessage("DormitoryId is required.");

        RuleFor(command => command.RoomDto)
            .NotNull().WithMessage("RoomDto cannot be null.")
            .SetValidator(new RoomDtoValidator());
    }
}

public class RoomDtoValidator : AbstractValidator<RoomDto>
{
    public RoomDtoValidator()
    {
        RuleFor(dto => dto.Number)
            .NotEmpty().WithMessage("Room number is required.")
            .MaximumLength(5).WithMessage("Room number must not exceed 5 characters.");

        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Room category is required.")
            .MaximumLength(50).WithMessage("Room category must not exceed 50 characters.");

        RuleFor(dto => dto.Capacity)
            .GreaterThan(0).WithMessage("Room capacity must be greater than zero.")
            .LessThanOrEqualTo(3).WithMessage("Room capacity must not exceed 3.");

        RuleFor(dto => dto.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Room price must not be negative.")
            .PrecisionScale(18, 2, true).WithMessage("Room price must have a maximum of 18 digits and 2 decimal places.");
    }
}