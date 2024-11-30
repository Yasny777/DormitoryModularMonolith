namespace Reservations.Reservations.ValueObjects;

public class RoomInfo
{
    public string Number { get; }
    public decimal Price { get; }
    public int Capacity { get; }

    protected RoomInfo()
    {
    }

    private RoomInfo(string number, decimal price, int capacity)
    {
        Number = number;
        Price = price;
        Capacity = capacity;
    }

    public static RoomInfo Of(string number, decimal price, int capacity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        return new RoomInfo(number, price, capacity);
    }
}