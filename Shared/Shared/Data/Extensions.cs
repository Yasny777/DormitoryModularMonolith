using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigration<TContext, TSeeder>(this IApplicationBuilder app)
        where TContext : DbContext
        where TSeeder : IDataSeeder
    {
        MigrateDataBaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
        SeedDataAsync<TSeeder>(app.ApplicationServices).GetAwaiter().GetResult();
        return app;
    }

    private static async Task SeedDataAsync<TSeeder>(IServiceProvider serviceProvider) where TSeeder : IDataSeeder
    {
        using var scope = serviceProvider.CreateScope();
        var seeders = scope.ServiceProvider.GetServices<TSeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAllAsync();
        }
    }

    private static async Task MigrateDataBaseAsync<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        await context.Database.MigrateAsync();
    }
}