namespace Reservations.Reservations.Features.CreateReservation.Handler;

internal record CreateReservationCommand(Guid RoomId, Guid UserId, string SemesterName) : ICommand<CreateReservationResult>;