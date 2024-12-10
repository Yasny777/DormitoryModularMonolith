namespace Reservations.Reservations.Features.CreateSemester.Handler;

public record CreateSemesterCommand(SemesterDto SemesterDto) : ICommand<CreateSemesterResult>;