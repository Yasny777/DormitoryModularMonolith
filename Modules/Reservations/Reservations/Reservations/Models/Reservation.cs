using Reservations.Reservations.Events;
using Reservations.Reservations.ValueObjects;
using Shared.DDD;

namespace Reservations.Reservations.Models;

public class Reservation : Aggregate<Guid>
{
    public Guid RoomId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public ReservationStatus Status { get; private set; } = default!;

    public RoomInfo RoomInfo { get; private set; } = default!;


    public static Reservation Create(Guid id, Guid roomId, Guid userId)
    {
        var reservation = new Reservation()
        {
            Status = ReservationStatus.Confirmed,
            StartDate = new DateTime(2024, 2, 1).ToUniversalTime(), //! todo hard coded, should be get from db
            EndDate =  new DateTime(2024, 6, 30).ToUniversalTime(),
            RoomId = roomId,
            UserId = userId,
        };

        reservation.AddDomainEvent(new ReservationCreatedEvent(reservation));
        return reservation;
    }


    public void Cancel()
    {
        if (Status == ReservationStatus.Cancelled)
            throw new InvalidOperationException("Reservation is already canceled.");

        Status = ReservationStatus.Cancelled;

        // Emit Domain Event
        AddDomainEvent(new ReservationCancelledEvent(this));
    }
}