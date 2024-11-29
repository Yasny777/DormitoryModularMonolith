using MediatR;
using Shared.Contracts.CQRS;

namespace Shared.Events;

public record ReservationCreatedIntegrationEvent(Guid ReservationId, Guid UserId, Guid RoomId) : INotification
{
    
}