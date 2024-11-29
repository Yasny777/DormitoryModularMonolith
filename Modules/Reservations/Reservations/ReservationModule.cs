using Reservations.Data;
using Reservations.Data.Redis;

namespace Reservations;

public static class ReservationModule
{
    public static IServiceCollection AddReservationModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ReservationDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddSingleton(RedLockFactoryProvider.CreateRedLockFactory(configuration.GetConnectionString("Redis")!));

        return services;
    }

    public static IApplicationBuilder UseReservationModule(this IApplicationBuilder app)
    {
        app.UseMigration<ReservationDbContext>();
        return app;
    }
}