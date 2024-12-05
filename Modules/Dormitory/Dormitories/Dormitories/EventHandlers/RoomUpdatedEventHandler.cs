using Dormitories.Dormitories.Events;
using Microsoft.Extensions.Logging;

namespace Dormitories.Dormitories.EventHandlers;

public class RoomUpdatedEventHandler(ILogger<RoomUpdatedEvent> logger)
    : INotificationHandler<RoomUpdatedEvent>
{
    public Task Handle(RoomUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // todo integrate with Reservation module to update price, capacity etc for active reservations in that room
        throw new NotImplementedException();
    }
}