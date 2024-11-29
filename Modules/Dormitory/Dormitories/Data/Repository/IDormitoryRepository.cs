using Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;
using Dormitories.Dormitories.Models;

namespace Dormitories.Data.Repository;

public interface IDormitoryRepository
{
    Task<List<Dormitory>> GetDormitories(bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<Guid> CreateDormitory(Dormitory dormitory, CancellationToken cancellationToken);

    Task<List<Room>> GetRoomsInDormitoryByQuery(GetRoomsInDormitoryQuery query, bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<long> GetTotalRoomCountInDormitory(Guid id, CancellationToken cancellationToken);
    Task<Dormitory?> GetDormitoryById(Guid dormitoryId, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<Room?> GetRoomById(Guid roomId, CancellationToken cancellationToken);

    Task<Dormitory?> GetDormitoryByRoomId(Guid roomId, CancellationToken cancellationToken);
}