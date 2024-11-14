using Microsoft.AspNetCore.Builder;

namespace User;

public static class UserModule
{
    public static IServiceCollection AddUserModule(this IServiceCollection services,
        IConfiguration configuration)
    {

        return services;
    }

    public static IApplicationBuilder UseUserModule(this IApplicationBuilder app)
    {
        return app;
    }
}