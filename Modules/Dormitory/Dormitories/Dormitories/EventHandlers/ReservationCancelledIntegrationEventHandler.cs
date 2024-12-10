using Dormitories.Dormitories.Features.RemoveOccupantFromRoom.Handler;

namespace Dormitories.Dormitories.EventHandlers;

public class ReservationCancelledIntegrationEventHandler(
    ISender sender,
    ILogger<ReservationCancelledIntegrationEventHandler> logger)
    : INotificationHandler<ReservationCancelledIntegrationEvent>
{
    public async Task Handle(ReservationCancelledIntegrationEvent request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", request.GetType().Name);

        var command = new RemoveOccupantFromRoomCommand(request.RoomId, request.UserId);
        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            logger.LogError("Error Removing Occupant {OccupantId} from room: {RoomId}", request.UserId, request.RoomId);
        }


        logger.LogInformation(
            "Reservation {ReservationId} for Occupant {OccupantId} - successfully cancelled from Room {RoomId}",
            request.ReservationId, request.UserId, request.RoomId);
    }
}