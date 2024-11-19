﻿using Dormitories.Data.Extensions;
using Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;
using Dormitories.Dormitories.Models;
using Microsoft.EntityFrameworkCore;

namespace Dormitories.Data.Repository;

public class DormitoryRepository(DormitoryDbContext dbContext) : IDormitoryRepository
{
    public async Task<List<Dormitory>> GetDormitories(bool asNoTracking = true, CancellationToken cancellationToken = default)
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

    public async Task<List<Room>> GetRoomsInDormitoryByQuery(GetRoomsInDormitoryQuery query, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var roomsQuery = dbContext.Rooms.Where(r => r.DormitoryId == query.DormitoryId);
        if (asNoTracking) roomsQuery = roomsQuery.AsNoTracking();
        // sorting
        roomsQuery = roomsQuery.ApplySorting(query.SortBy, query.SortDirection);

        // todo include users?

        var rooms = await roomsQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return rooms;
    }

    public async Task<long> GetTotalRoomCountInDormitory(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms.Where(r => r.DormitoryId == id).LongCountAsync(cancellationToken);
    }
}