namespace Reservations.Reservations.ValueObjects;

public class RoomInfo
{
    public string Number { get; }
    public string Price { get; }
    public int Capacity { get; }

    protected RoomInfo()
    {
    }

    private RoomInfo(string number, string price, int capacity)
    {
        Number = number;
        Price = price;
        Capacity = capacity;
    }

    public static RoomInfo Of(string number, string price, int capacity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        ArgumentException.ThrowIfNullOrWhiteSpace(price);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        return new RoomInfo(number, price, capacity);
    }
}