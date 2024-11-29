using Reservations.Reservations.Models;
using Shared.DDD;

namespace Reservations.Reservations.Events;

public record ReservationCancelledEvent(Reservation Reservation) : IDomainEvent
{
    
}