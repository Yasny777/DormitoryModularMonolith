namespace Dormitories.Dormitories.ValueObjects;

public record Address
{
    // Value Objecty nie maja setterów bo sa immutable
    public string Street { get; } = default!;
    public string City { get; } = default!;
    public string ZipCode { get; } = default!;

    protected Address()
    {
    }

    private Address(string street, string city, string zipCode)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public static Address Of(string street, string city, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

        return new Address(street, city, zipCode);
    }
}