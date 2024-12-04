using Dormitories.Dormitories.Events;
using Microsoft.Extensions.Logging;

namespace Dormitories.Dormitories.EventHandlers;

public class RoomUpdatedEventHandler(ILogger<RoomUpdatedEvent> logger)
    : INotificationHandler<RoomUpdatedEvent>
{
    public Task Handle(RoomUpdatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}