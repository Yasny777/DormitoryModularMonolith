using Reservations.Reservations.ValueObjects;

namespace Reservations.Reservations.Dto;

public record ReservationDto(Guid RoomId, Guid UserId, DateTime StartDate, DateTime EndDate, ReservationStatus Status);