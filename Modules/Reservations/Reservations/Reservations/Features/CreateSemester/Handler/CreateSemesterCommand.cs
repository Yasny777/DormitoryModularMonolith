namespace Reservations.Reservations.Features.CreateSemester.Handler;

internal record CreateSemesterCommand(SemesterDto SemesterDto) : ICommand<CreateSemesterResult>;