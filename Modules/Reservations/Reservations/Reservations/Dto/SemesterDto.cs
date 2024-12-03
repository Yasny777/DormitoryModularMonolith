namespace Reservations.Reservations.Dto;

public record SemesterDto(string Name, int Number, DateTime StartDate, DateTime EndDate, bool IsActive);