namespace Reservations.Reservations.Features.CreateReservation.Handler;

public record CreateReservationCommand(Guid RoomId, Guid UserId, string SemesterName, string UserRole) : ICommand<CreateReservationResult>;