using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Data;
using Shared.Data.Interceptors;
using User.Data;
using User.User.Modules;

namespace User;

public static class UserModule
{
    public static IServiceCollection AddUserModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");


        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<MyIdentityDbContext>((serviceProvider, opt) =>
        {
            opt.UseNpgsql(connectionString);
            opt.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        services.AddAuthorization();
        services.AddIdentityApiEndpoints<AppUser>(cfg =>
            {
                cfg.
            })
            .AddEntityFrameworkStores<MyIdentityDbContext>();
        return services;
    }

    public static IApplicationBuilder UseUserModule(this IApplicationBuilder app)
    {
        app.UseMigration<MyIdentityDbContext>();


        return app;
    }
}