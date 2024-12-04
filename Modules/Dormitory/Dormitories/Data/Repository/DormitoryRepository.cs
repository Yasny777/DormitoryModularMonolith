using Dormitories.Data.Extensions;
using Dormitories.Data.Repository.Queries;
using Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;
using Dormitories.Dormitories.Models;
using Microsoft.EntityFrameworkCore;

namespace Dormitories.Data.Repository;

internal class DormitoryRepository(DormitoryDbContext dbContext) : IDormitoryRepository
{
    public async Task<List<Dormitory>> GetDormitories(bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = asNoTracking ? dbContext.Dormitories.AsNoTracking() : dbContext.Dormitories;

        var dormitories = await query.ToListAsync(cancellationToken);

        return dormitories;
    }

    public async Task<Guid> CreateDormitory(Dormitory dormitory, CancellationToken cancellationToken)
    {
        dbContext.Dormitories.Add(dormitory);
        await dbContext.SaveChangesAsync(cancellationToken);
        return dormitory.Id;
    }

    public async Task<RoomQueryResult> GetRoomsInDormitoryByQuery(GetRoomsInDormitoryQuery query, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var roomsQuery = dbContext.Rooms
            .Include(r => r.Occupants)
            .Where(r => r.DormitoryId == query.DormitoryId);

        if (asNoTracking) roomsQuery = roomsQuery.AsNoTracking();

        if (query.PriceFrom.HasValue)
        {
            roomsQuery = roomsQuery.Where(r => r.Price >= query.PriceFrom);
        }

        if (query.PriceTo.HasValue)
        {
            roomsQuery = roomsQuery.Where(r => r.Price <= query.PriceTo);
        }

        if (query.Capacity.HasValue)
        {
            roomsQuery = roomsQuery.Where(r => r.Capacity == query.Capacity);
        }

        if (!string.IsNullOrEmpty(query.Category))
        {
            roomsQuery = roomsQuery.Where(r => r.Category == query.Category);
        }

        // sorting
        roomsQuery = roomsQuery.ApplySorting(query.SortBy, query.SortDirection);

        var totalCount = await roomsQuery.LongCountAsync(cancellationToken);
        // todo include users?
        var rooms = await roomsQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new RoomQueryResult(rooms, totalCount);
    }
    public async Task<Dormitory?> GetDormitoryById(Guid dormitoryId, CancellationToken cancellationToken)
    {
        return await dbContext.Dormitories.Include(d => d.Rooms)
            .Where(d => d.Id == dormitoryId).SingleOrDefaultAsync(cancellationToken) ?? null;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Room?> GetRoomById(Guid roomId, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms.Include(r => r.Occupants)
            .SingleOrDefaultAsync(r => r.Id == roomId, cancellationToken) ?? null;
    }

    public async Task<Dormitory?> GetDormitoryByRoomId(Guid roomId, CancellationToken cancellationToken)
    {
        return await dbContext.Dormitories
            .Include(d => d.Rooms)
            .ThenInclude(r => r.Occupants)
            .FirstOrDefaultAsync(d => d.Rooms.Any(r => r.Id == roomId), cancellationToken);
    }
}