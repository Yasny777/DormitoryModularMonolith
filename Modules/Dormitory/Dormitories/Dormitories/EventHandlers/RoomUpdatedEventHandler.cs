using Dormitories.Dormitories.Events;
using Microsoft.Extensions.Logging;
using Shared.Events;

namespace Dormitories.Dormitories.EventHandlers;

public class RoomUpdatedEventHandler(ILogger<RoomUpdatedEvent> logger, IPublisher publisher)
    : INotificationHandler<RoomUpdatedEvent>
{
    public Task Handle(RoomUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        var integrationEvent =
            new RoomUpdatedIntegrationEvent(notification.Room.Id, notification.Room.Number, notification.Room.Capacity,
                notification.Room.Price);

        publisher.Publish(integrationEvent, cancellationToken);

        return Task.CompletedTask;
    }
}