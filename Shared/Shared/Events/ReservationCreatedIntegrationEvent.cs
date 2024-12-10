using MediatR;

namespace Shared.Events;

public record ReservationCreatedIntegrationEvent(Guid ReservationId, Guid UserId, Guid RoomId) : INotification
{
    
}