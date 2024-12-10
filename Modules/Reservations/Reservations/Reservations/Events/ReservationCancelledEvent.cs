namespace Reservations.Reservations.Events;

public record ReservationCancelledEvent(Reservation Reservation) : IDomainEvent
{
    
}