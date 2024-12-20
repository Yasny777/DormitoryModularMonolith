namespace Dormitories.Dormitories.EventHandlers;

public class RoomAddedEventHandler(ILogger<RoomAddedEventHandler> logger) : INotificationHandler<RoomAddedEvent>
{
    public async Task Handle(RoomAddedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        logger.LogInformation("Room id {RoomId} with number {RoomNumber} successfully created!", notification.Room.Id,
            notification.Room.Number);
    }
}