using FluentValidation;

namespace Dormitories.Dormitories.Features.CreateDormitory.Handler;

internal class CreateDormitoryCommandValidator : AbstractValidator<CreateDormitoryCommand>
{
    public CreateDormitoryCommandValidator()
    {
        RuleFor(x => x.DormitoryDto).NotNull().WithMessage("DormitoryDto can't be null");
        RuleFor(x => x.DormitoryDto.ContactEmail).EmailAddress().WithMessage("Invalid email address");
        RuleFor(x => x.DormitoryDto.Category)
            .Must(category =>
                new[] { DormitoryCategories.Cheap, DormitoryCategories.Normal, DormitoryCategories.Prestige }
                    .Contains(category)).WithMessage("Category must be one of the specified");
        RuleFor(x => x.DormitoryDto.Name).NotNull().WithMessage("Name can't be null");
    }
}