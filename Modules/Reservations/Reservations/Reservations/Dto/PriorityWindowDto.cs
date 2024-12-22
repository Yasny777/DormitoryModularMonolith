namespace Reservations.Reservations.Dto;

public record PriorityWindowDto(List<string> RoleNames, DateTime StartDateTime, DateTime EndDateTime)
{
}