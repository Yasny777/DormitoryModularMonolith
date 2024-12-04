using Microsoft.Extensions.Logging;
using Reservations.Reservations.Features.CancelReservation.Handler;
using Reservations.Reservations.ValueObjects;
using Shared.Events;

namespace Reservations.Reservations.EventHandlers;

public class RoomRemovedIntegrationEventHandler(
    ILogger<RoomRemovedIntegrationEventHandler> logger,
    ISender sender,
    ReservationDbContext dbContext)
    : INotificationHandler<RoomRemovedIntegrationEvent>
{
    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", notification.GetType().Name);

        // get all reservations with roomId
        var reservations = await dbContext.Reservations
            .Where(r => r.RoomId == notification.RoomId && r.Status == ReservationStatus.Active)
            .ToListAsync(cancellationToken);


        if (!reservations.Any())
        {
            logger.LogInformation("No reservations found for RoomId: {RoomId}", notification.RoomId);
            return;
        }

        logger.LogInformation("Found {Count} reservations for RoomId: {RoomId}", reservations.Count, notification.RoomId);

        foreach (var reservation in reservations)
        {
            var command = new CancelReservationCommand(reservation.UserId.ToString(), reservation.Id);
            var result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                logger.LogError("Failed to cancel reservation with Id: {ReservationId}", reservation.Id);
            }
            else
            {
                logger.LogInformation("Successfully canceled reservation with Id: {ReservationId}", reservation.Id);
            }
        }
    }
}