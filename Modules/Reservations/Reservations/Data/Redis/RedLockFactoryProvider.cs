using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Reservations.Data.Redis;

public static class RedLockFactoryProvider
{
    public static RedLockFactory CreateRedLockFactory(string connectionString)
    {
        // Konfiguracja połączenia z Redis
        var multiplexers = new List<RedLockMultiplexer>
        {
            ConnectionMultiplexer.Connect(connectionString)
        };

        // Tworzenie fabryki RedLock
        return RedLockFactory.Create(multiplexers);
    }
}