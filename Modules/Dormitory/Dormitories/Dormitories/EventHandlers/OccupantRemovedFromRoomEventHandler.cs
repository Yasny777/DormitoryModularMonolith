using Dormitories.Dormitories.Events;
using Microsoft.Extensions.Logging;

namespace Dormitories.Dormitories.EventHandlers;

public class OccupantRemovedFromRoomEventHandler(ILogger<OccupantRemovedFromRoomEventHandler> logger)
    : INotificationHandler<OccupantRemovedFromRoomEvent>
{
    public Task Handle(OccupantRemovedFromRoomEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
