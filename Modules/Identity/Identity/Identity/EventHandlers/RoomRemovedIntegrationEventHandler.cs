using Identity.Identity.Features.CancelReservationAndRoom.Handler;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Events;

namespace Identity.Identity.EventHandlers;

//todo this same as in reservation module
public class RoomRemovedIntegrationEventHandler(
    ILogger<RoomRemovedIntegrationEventHandler> logger,
    ISender sender)
    : INotificationHandler<RoomRemovedIntegrationEvent>
{
    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", notification.GetType().Name);

        var result = await sender.Send(new CancelReservationAndRoomCommand(notification.RoomId), cancellationToken);

        if (result.IsSuccess)
            logger.LogInformation("Reservations for Room {RoomId} cancelled succeed", notification.RoomId);

        logger.LogError("Reservations for Room {RoomId} cannot been cancelled", notification.RoomId);
    }
}