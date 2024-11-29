using Reservations.Reservations.Models;
using Shared.DDD;

namespace Reservations.Reservations.Events;

public record ReservationCreatedEvent(Reservation Reservation) : IDomainEvent
{
}