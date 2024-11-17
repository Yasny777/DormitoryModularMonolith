using Dormitories.Dormitories.Models;

namespace Dormitories.Data.Repository;

public interface IDormitoryRepository
{
    Task<List<Dormitory>> GetDormitories(bool asNoTracking = true, CancellationToken cancellationToken = default);

}