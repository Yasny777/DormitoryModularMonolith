﻿using Reservations.Reservations.Features.CancelReservationsByRoomId.Handler;

namespace Reservations.Reservations.EventHandlers;

public class RoomRemovedIntegrationEventHandler(
    ILogger<RoomRemovedIntegrationEventHandler> logger,
    ISender sender)
    : INotificationHandler<RoomRemovedIntegrationEvent>
{
    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", notification.GetType().Name);

        var result = await sender.Send(new CancelReservationsByRoomIdCommand(notification.RoomId), cancellationToken);

        if (result.IsSuccess) logger.LogInformation("Reservations for Room {RoomId} cancelled succeed", notification.RoomId);
        else logger.LogError("Reservations for Room {RoomId} cannot been cancelled", notification.RoomId);
    }

}