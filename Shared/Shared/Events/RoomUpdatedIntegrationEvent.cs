using MediatR;

namespace Shared.Events;

public record RoomUpdatedIntegrationEvent(Guid RoomId, string Number, int Capacity, decimal Price) : INotification
{
    
}