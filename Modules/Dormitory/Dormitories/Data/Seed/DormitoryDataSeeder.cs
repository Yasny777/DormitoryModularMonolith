using Microsoft.EntityFrameworkCore;
using Shared.Data.Seed;

namespace Dormitories.Data.Seed;

public class DormitoryDataSeeder(DormitoryDbContext dbContext) : IDataSeeder
{
    //todo do all seeders
    public async Task SeedAllAsync()
    {
        if (!await dbContext.Dormitories.AnyAsync())
        {
            await dbContext.Dormitories.AddRangeAsync(InitialData.Dormitories);
            await dbContext.SaveChangesAsync();
        }
    }
}