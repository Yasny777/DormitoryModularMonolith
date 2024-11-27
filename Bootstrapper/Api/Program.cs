using Carter;
using Identity;
using Identity.Identity.Constants;
using Identity.Identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Exceptions.Handler;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.ReadFrom.Configuration(ctx.Configuration);
});
// Configure services

var dormitoryAssembly = typeof(DormitoryModule).Assembly;
var identityAssembly = typeof(IdentityModule).Assembly;

// Common services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCarterWithAssemblies(dormitoryAssembly, identityAssembly);

builder.Services.AddMediatRWithAssemblies(dormitoryAssembly, identityAssembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Adres frontendu
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Umożliwia obsługę ciasteczek
    });
});

// Services for modules
builder.Services
    .AddDormitoryModule(configuration)
    .AddReservationModule(configuration)
    .AddIdentityModule(configuration);

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Wymaganie dodawania tokena do zapytań
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
var app = builder.Build();
// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseExceptionHandler(cfg => {});
app.UseCors("AllowFrontend");
app.UseIdentityModule();
app.MapCarter();
app.UseDormitoryModule()
    .UseReservationModule();

app.Run();