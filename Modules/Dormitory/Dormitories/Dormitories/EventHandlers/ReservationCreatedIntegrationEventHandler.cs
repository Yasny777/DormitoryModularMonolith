﻿using Dormitories.Dormitories.Features.AddOcuupantToRoom.Handler;

namespace Dormitories.Dormitories.EventHandlers;

internal class ReservationCreatedIntegrationEventHandler(
    ISender sender,
    ILogger<ReservationCreatedIntegrationEventHandler> logger)
    : INotificationHandler<ReservationCreatedIntegrationEvent>
{
    public async Task Handle(ReservationCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", request.GetType().Name);

        var command = new AddOccupantToRoomCommand(request.RoomId, request.UserId);
        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            logger.LogError("Error Adding Occupant {OccupantId} to room: {RoomId}", request.UserId, request.RoomId);
        }


        logger.LogInformation(
            "Reservation {ReservationId} for Occupant {OccupantId} - successfully added to Room {RoomId}",
            request.ReservationId, request.UserId, request.RoomId);
    }
}