using Dormitories.Data.Repository;
using Dormitories.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Data;
using Shared.Data.Interceptors;
using Shared.Data.Seed;

namespace Dormitories;

public static class DormitoryModule
{
    public static IServiceCollection AddDormitoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<IDormitoryRepository, DormitoryRepository>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<DormitoryDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<DormitoryDataSeeder>();
        return services;
    }


    public static IApplicationBuilder UseDormitoryModule(this IApplicationBuilder app)
    {
        app.UseMigration<DormitoryDbContext, DormitoryDataSeeder>();
        return app;
    }
}