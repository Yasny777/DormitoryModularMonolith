using FluentValidation;
using Reservations.Reservations.Features.CreateSemester.Endpoint;

namespace Reservations.Reservations.Features.CreateSemester.Handler;

public class CreateSemesterCommandValidator : AbstractValidator<CreateSemesterCommand>
{
    public CreateSemesterCommandValidator()
    {
        RuleFor(x => x.SemesterDto).NotNull().WithMessage("SemesterDto cannot be null");
        RuleFor(x => x.SemesterDto.Name).NotEmpty().WithMessage("Semester name cannot be empty");
        RuleFor(x => x.SemesterDto.StartDate).LessThan(x => x.SemesterDto.EndDate).WithMessage("StartDate must be before EndDate");
        RuleFor(x => x.SemesterDto.Number).GreaterThanOrEqualTo(1).LessThanOrEqualTo(2).WithMessage("Semester number must be greater than 0 and less than 3");
    }
}