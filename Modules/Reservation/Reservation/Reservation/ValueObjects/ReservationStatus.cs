namespace Reservation.Reservation.ValueObjects;

public class ReservationStatus
{
    public static readonly ReservationStatus Pending = new("Pending");
    public static readonly ReservationStatus Confirmed = new("Confirmed");
    public static readonly ReservationStatus Cancelled = new("Cancelled");
    public static readonly ReservationStatus Expired = new("Expired");

    public string Value { get; }

    private ReservationStatus(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;
}