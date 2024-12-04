using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Events;

namespace Identity.Identity.EventHandlers;

public class RoomRemovedIntegrationEventHandler(
    ILogger<RoomRemovedIntegrationEventHandler> logger,
    UserManager<AppUser> userManager)
    : INotificationHandler<RoomRemovedIntegrationEvent>
{
    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", notification.GetType().Name);


        var users = await userManager.Users
            .Where(user => user.RoomId == notification.RoomId)
            .ToListAsync(cancellationToken);

        if (!users.Any())
        {
            logger.LogInformation("No users found for RoomId: {RoomId}", notification.RoomId);
            return;
        }

        logger.LogInformation("Found {UserCount} users associated with RoomId: {RoomId}", users.Count,
            notification.RoomId);

        foreach (var user in users)
        {
            user.RoomId = null;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                logger.LogInformation("Successfully removed RoomId for UserId: {UserId}", user.Id);
            }
            else
            {
                logger.LogError("Failed to remove RoomId for UserId: {UserId}.",
                    user.Id);
            }
        }

        logger.LogInformation("Finished handling RoomRemovedIntegrationEvent for RoomId: {RoomId}",
            notification.RoomId);
    }
}