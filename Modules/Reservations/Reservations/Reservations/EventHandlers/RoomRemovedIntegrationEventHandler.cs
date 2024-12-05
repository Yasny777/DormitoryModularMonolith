using Microsoft.Extensions.Logging;
using Reservations.Reservations.Features.CancelReservationsByRoomId;
using Shared.Events;

namespace Reservations.Reservations.EventHandlers;

public class RoomRemovedIntegrationEventHandler(
    ILogger<RoomRemovedIntegrationEventHandler> logger,
    ISender sender)
    : INotificationHandler<RoomRemovedIntegrationEvent>
{
    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", notification.GetType().Name);

        await sender.Send(new CancelReservationsByRoomIdQuery(notification.RoomId), cancellationToken);


    }

}