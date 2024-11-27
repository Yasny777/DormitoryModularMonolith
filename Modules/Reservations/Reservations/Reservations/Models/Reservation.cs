using Reservations.Reservations.ValueObjects;
using Shared.DDD;

namespace Reservations.Reservations.Models;

public class Reservation : Aggregate<Guid>
{
    public Guid RoomId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public ReservationStatus Status { get; private set; }


    public static Reservation Create()
    {

    }
}