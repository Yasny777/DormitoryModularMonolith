using Dormitories.Dormitories.Events;
using Microsoft.Extensions.Logging;
using Shared.Events;

namespace Dormitories.Dormitories.EventHandlers;

public class RoomRemovedEventHandler(ILogger<RoomRemovedEvent> logger, IPublisher publisher)
    : INotificationHandler<RoomRemovedEvent>
{
    public Task Handle(RoomRemovedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        var integrationEvent = new RoomRemovedIntegrationEvent(notification.Room.Id);

        publisher.Publish(integrationEvent, cancellationToken);

        return Task.CompletedTask;
    }
}