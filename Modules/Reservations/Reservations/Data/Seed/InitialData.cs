namespace Reservations.Data.Seed;

public class InitialData
{
    public static Semester Semester => Semester.Create(new Guid(),
        "Semestr Letni 2024/2025",
        2,
        new DateTime(2025, 3, 3).ToUniversalTime(),
        new DateTime(2025, 6, 30).ToUniversalTime(),
        true);
}