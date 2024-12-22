namespace Reservations.Reservations.EventHandlers;

public class ReservationCreatedEventHandler(ILogger<ReservationCreatedEventHandler> logger, IPublisher publisher)
    : INotificationHandler<ReservationCreatedEvent>
{
    public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        var reservationCreatedIntegrationEvent = new ReservationCreatedIntegrationEvent(
            notification.Reservation.Id,
            notification.Reservation.UserId,
            notification.Reservation.RoomId);

        await publisher.Publish(reservationCreatedIntegrationEvent, cancellationToken);
    }
}