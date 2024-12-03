namespace Reservations.Reservations.Features.GetActiveSemester.Endpoint;

public abstract record GetActiveSemesterResponse(Guid Id, string Name, DateTime StartDate, DateTime EndDate);