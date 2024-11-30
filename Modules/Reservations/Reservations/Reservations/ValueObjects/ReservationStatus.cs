namespace Reservations.Reservations.ValueObjects;

public class ReservationStatus
{
    public static readonly ReservationStatus Pending = new("Pending");
    public static readonly ReservationStatus Active = new("Active");
    public static readonly ReservationStatus Cancelled = new("Cancelled");
    public static readonly ReservationStatus Expired = new("Expired");

    public string Value { get; }

    private ReservationStatus(string value)
    {
        Value = value;
    }

    public static ReservationStatus FromValue(string value)
    {
        return value switch
        {
            "Pending" => Pending,
            "Active" => Active,
            "Cancelled" => Cancelled,
            "Expired" => Expired,
            _ => throw new ArgumentException($"Invalid status value: {value}")
        };
    }
    public override string ToString() => Value;
}