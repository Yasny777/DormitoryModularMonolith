using RedLockNet;
using RedLockNet.SERedis;

namespace Reservations.Reservations.Services;

public interface IDistributedLockService
{
    Task<IRedLock> AcquireLockAsync(string resource, CancellationToken cancellationToken);
}

public class DistributedLockService(RedLockFactory redLockFactory) : IDistributedLockService
{
    public async Task<IRedLock> AcquireLockAsync(string resource, CancellationToken cancellationToken)
    {
        var lockExpiry = TimeSpan.FromSeconds(10); // Czas blokady zasobu
        var waitTime = TimeSpan.FromSeconds(2); // Czas próby uzyskania dostępu
        var retryTime = TimeSpan.FromMilliseconds(500); // Czas pomiędzy próbami uzyskania dostępu

        var redLock = await redLockFactory.CreateLockAsync(resource, lockExpiry, waitTime, retryTime, cancellationToken);

        if (!redLock.IsAcquired)
        {
            throw new Exception($"Failed to acquire lock for resource {resource}");
        }

        return redLock;
    }
}
