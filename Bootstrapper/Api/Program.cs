using Carter;
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


// Common services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCarterWithAssemblies(dormitoryAssembly);

builder.Services.AddMediatRWithAssemblies(dormitoryAssembly);

// Services for modules
builder.Services
    .AddDormitoryModule(configuration)
    .AddReservationModule(configuration)
    .AddUserModule(configuration);



var app = builder.Build();
// Configure HTTP request pipeline

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(cfg => {});

app.UseDormitoryModule()
    .UseReservationModule()
    .UseUserModule();

app.Run();