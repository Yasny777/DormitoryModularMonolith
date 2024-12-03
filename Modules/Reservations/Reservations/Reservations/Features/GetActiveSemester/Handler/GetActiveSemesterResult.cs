namespace Reservations.Reservations.Features.GetActiveSemester.Handler;

public record GetActiveSemesterResult(Guid Id, string Name, DateTime StartDate, DateTime EndDate, bool IsActive);