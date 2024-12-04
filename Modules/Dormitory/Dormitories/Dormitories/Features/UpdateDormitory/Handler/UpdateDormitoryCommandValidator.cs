using Dormitories.Dormitories.Features.UpdateDormitory.Endpoint;
using FluentValidation;

namespace Dormitories.Dormitories.Features.UpdateDormitory.Handler;

internal class UpdateDormitoryCommandValidator : AbstractValidator<UpdateDormitoryCommand>
{
    public UpdateDormitoryCommandValidator()
    {
        RuleFor(x => x.DormitoryId)
            .NotEmpty()
            .WithMessage("Dormitory ID cannot be empty.");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Dormitory name cannot exceed 100 characters.");

        RuleFor(x => x.Category)
            .Must(category =>
                string.IsNullOrWhiteSpace(category) || // Allow empty category
                new[] { "cheap", "normal", "prestige" }.Contains(category))
            .WithMessage("Category must be one of: 'cheap', 'normal', or 'prestige'.");
    }
}