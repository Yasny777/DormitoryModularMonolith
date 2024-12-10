using Shared.Data.Seed;

namespace Reservations.Data.Seed;

public class ReservationDataSeeder(ReservationDbContext dbContext) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if (!await dbContext.Semesters.AnyAsync())
        {
            await dbContext.Semesters.AddRangeAsync(InitialData.Semester);
            await dbContext.SaveChangesAsync();
        }
    }
}