using System.Linq.Expressions;
using Dormitories.Dormitories.Models;
using Shared.Constants;

namespace Dormitories.Data.Extensions;

public static class QueryExtensions
{
    public static IQueryable<Room> ApplySorting(this IQueryable<Room> query, string sortBy,
        SortDirection sortDirection)
    {
        if (string.IsNullOrEmpty(sortBy))
        {
            return query;
        }

        var columnNameMap = new Dictionary<string, Expression<Func<Room, object>>>()
        {
            { nameof(Room.Number).ToLower(), r => r.Number },
            { nameof(Room.Price).ToLower(), r => r.Price },
            { nameof(Room.Capacity).ToLower(), r => r.Capacity }
        };

        if (!columnNameMap.ContainsKey(sortBy.ToLower()))
        {
            return query;
        }

        query = sortDirection == SortDirection.Asc ?
            query.OrderBy(columnNameMap[sortBy.ToLower()])
            : query.OrderByDescending(columnNameMap[sortBy.ToLower()]);

        return query;
    }
}