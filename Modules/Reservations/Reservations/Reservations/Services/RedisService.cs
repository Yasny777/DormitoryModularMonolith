using Dormitories.Contracts.Dormitories.GetRoomById;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace Reservations.Reservations.Services;

public interface IRedisService
{
    Task<string> GetOrSetRoomCapacityAsync(Guid roomId, CancellationToken cancellationToken);
    Task<int> IncrementOccupantsAsync(Guid roomId, string capacity, CancellationToken cancellationToken);
    Task DecrementOccupantsAsync(Guid roomId, CancellationToken cancellationToken);
}

public class RedisService(IDistributedCache redis, ISender sender) : IRedisService
{
    public async Task<string> GetOrSetRoomCapacityAsync(Guid roomId, CancellationToken cancellationToken)
    {
        var redisKeyCapacity = $"room:{roomId}:capacity";
        var capacity = await redis.GetStringAsync(redisKeyCapacity, cancellationToken);

        if (!capacity.IsNullOrEmpty()) return capacity;

        var room = await sender.Send(new GetRoomByIdQuery(roomId), cancellationToken);
        await redis.SetStringAsync(redisKeyCapacity, room.RoomDto.Capacity.ToString(), cancellationToken);
        capacity = room.RoomDto.Capacity.ToString();

        return capacity;
    }

    public async Task<int> IncrementOccupantsAsync(Guid roomId, string capacity, CancellationToken cancellationToken)
    {
        var redisKeyOccupants = $"room:{roomId}:occupants";
        var currentOccupants = await redis.GetStringAsync(redisKeyOccupants, cancellationToken);

        var occupantsCount = currentOccupants.IsNullOrEmpty() ? 0 : int.Parse(currentOccupants);
        var capacityCount = int.Parse(capacity);

        if (occupantsCount >= capacityCount)
        {
            throw new Exception("Room is fully booked.");
        }

        occupantsCount += 1;
        await redis.SetStringAsync(redisKeyOccupants, occupantsCount.ToString(), cancellationToken);

        return occupantsCount;
    }

    public async Task DecrementOccupantsAsync(Guid roomId, CancellationToken cancellationToken)
    {
        var redisKeyOccupants = $"room:{roomId}:occupants";
        var currentOccupants = await redis.GetStringAsync(redisKeyOccupants, cancellationToken);
        var occupantsCount = int.Parse(currentOccupants) - 1;
        await redis.SetStringAsync(redisKeyOccupants, occupantsCount.ToString(), cancellationToken);
    }
}
