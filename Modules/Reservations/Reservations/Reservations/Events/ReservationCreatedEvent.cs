namespace Reservations.Reservations.Events;

public record ReservationCreatedEvent(Reservation Reservation) : IDomainEvent
{
}