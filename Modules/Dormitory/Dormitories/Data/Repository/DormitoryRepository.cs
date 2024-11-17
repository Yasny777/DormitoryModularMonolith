using Dormitories.Dormitories.Models;
using Microsoft.EntityFrameworkCore;

namespace Dormitories.Data.Repository;

public class DormitoryRepository(DormitoryDbContext dbContext) : IDormitoryRepository
{
    public async Task<List<Dormitory>> GetDormitories(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Dormitories;

        if (asNoTracking) query.AsNoTracking();

        var dormitories = await query.ToListAsync(cancellationToken);

        return dormitories;
    }
}