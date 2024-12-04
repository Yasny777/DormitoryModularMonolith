using MediatR;

namespace Shared.Events;

public record RoomUpdatedIntegrationEvent() : INotification
{
    
}