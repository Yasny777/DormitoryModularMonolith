using Dormitories.Dormitories.Events;
using Microsoft.Extensions.Logging;

namespace Dormitories.Dormitories.EventHandlers;

public class OccupantAddedToRoomEventHandler(ILogger<OccupantAddedToRoomEventHandler> logger) : INotificationHandler<OccupantAddedToRoomEvent>
{
    public Task Handle(OccupantAddedToRoomEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}