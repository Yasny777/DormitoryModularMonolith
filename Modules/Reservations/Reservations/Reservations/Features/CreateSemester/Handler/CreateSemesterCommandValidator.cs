namespace Reservations.Reservations.Features.CreateSemester.Handler;

public class CreateSemesterCommandValidator : AbstractValidator<CreateSemesterCommand>
{
    public CreateSemesterCommandValidator()
    {
        RuleFor(x => x.SemesterDto).NotNull().WithMessage("SemesterDto cannot be null")
            .SetValidator(new SemesterDtoValidator());
    }
}


public class SemesterDtoValidator : AbstractValidator<SemesterDto>
{
    public SemesterDtoValidator()
    {
        RuleFor(d => d.Name).NotEmpty().WithMessage("Semester name cannot be empty");
        RuleFor(d => d.StartDate)
            .LessThan(d => d.EndDate)
            .WithMessage("StartDate must be before EndDate");
        RuleFor(d => d.Number)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(2)
            .WithMessage("Semester number must be greater than 0 and less than 3");
    }
}
