using Dormitories.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Shared.Data;
using Shared.Data.Seed;

namespace Dormitories;

public static class DormitoryModule
{
    public static IServiceCollection AddDormitoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<DormitoryDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IDataSeeder, DormitoryDataSeeder>();
        return services;
    }


    public static IApplicationBuilder UseDormitoryModule(this IApplicationBuilder app)
    {
        app.UseMigration<DormitoryDbContext>();
        return app;
    }
}