using MediatR;
using Microsoft.Extensions.Logging;
using Reservations.Reservations.Events;
using Shared.Events;

namespace Reservations.Reservations.EventHandlers;

public class ReservationCreatedEventHandler(ILogger<ReservationCreatedEventHandler> logger, ISender sender)
    : INotificationHandler<ReservationCreatedEvent>
{
    public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        // integration event with Dormitory module to add user to Room and in User module assign room

        var reservationCreatedIntegrationEvent = new ReservationCreatedIntegrationEvent(
            notification.Reservation.Id,
            notification.Reservation.RoomId,
            notification.Reservation.UserId);

        // multicast do modułu Dormitory i Identity (User)
        await sender.Send(reservationCreatedIntegrationEvent, cancellationToken);
    }
}