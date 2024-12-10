namespace Reservations.Reservations.EventHandlers;

public class ReservationCreatedEventHandler(ILogger<ReservationCreatedEventHandler> logger, IPublisher publisher)
    : INotificationHandler<ReservationCreatedEvent>
{
    public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        // integration event with Dormitory module to add user to Room and in User module assign room

        var reservationCreatedIntegrationEvent = new ReservationCreatedIntegrationEvent(
            notification.Reservation.Id,
            notification.Reservation.UserId,
            notification.Reservation.RoomId);

        // multicast do modułu Dormitory i Identity (User)
        await publisher.Publish(reservationCreatedIntegrationEvent, cancellationToken);
    }
}