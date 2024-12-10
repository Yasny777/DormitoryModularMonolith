using Reservations.Reservations.Features.UpdateActiveReservationsByRoomId.Handler;

namespace Reservations.Reservations.EventHandlers;

public class RoomUpdatedIntegrationEventHandler(
    ILogger<RoomUpdatedIntegrationEventHandler> logger,
    ISender sender) : INotificationHandler<RoomUpdatedIntegrationEvent>
{
    public async Task Handle(RoomUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", notification.GetType().Name);

        var result =
            await sender.Send(
                new UpdateActiveReservationsByRoomIdCommand(
                    notification.RoomId,
                    notification.Number,
                    notification.Capacity,
                    notification.Price), cancellationToken);

        if (result.IsSuccess)
            logger.LogInformation("Reservations for Room {RoomId} updated succeed", notification.RoomId);
        else logger.LogError("Reservations for Room {RoomId} cannot been updated", notification.RoomId);
    }
}