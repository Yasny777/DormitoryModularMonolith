namespace Dormitories;

public static class DormitoryModule
{
    public static IServiceCollection AddDormitoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }


    public static IApplicationBuilder UseDormitoryModule(this IApplicationBuilder app)
    {
        return app;
    }
}