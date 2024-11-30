using MediatR;

namespace Shared.Events;

public record ReservationCancelledIntegrationEvent(Guid ReservationId, Guid UserId, Guid RoomId) : INotification
{
    
}