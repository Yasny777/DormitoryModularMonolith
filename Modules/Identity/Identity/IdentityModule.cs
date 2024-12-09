﻿using System.Text;
using Identity.Data;
using Identity.Data.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Shared.Data;
using Shared.Data.Interceptors;
using Shared.Data.Seed;

namespace Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddScoped<IDataSeeder, RolesDataSeeder>();
        services.AddScoped<IDataSeeder, UserDataSeeder>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<MyIdentityDbContext>((serviceProvider, opt) =>
        {
            opt.UseNpgsql(connectionString);
            opt.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 5,
                    RequireDigit = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };
            })
            .AddEntityFrameworkStores<MyIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ??
                        throw new InvalidOperationException(
                            "Key doesnt exist"))),
                    ClockSkew = TimeSpan.FromMinutes(2) //todo to change
                };
            });

        services.AddScoped<ITokenService, TokenService>();

        services.AddAuthorization();

        return services;
    }

    public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMigration<MyIdentityDbContext>();
        return app;
    }
}