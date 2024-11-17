using Carter;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Configure services

var dormitoryAssembly = typeof(DormitoryModule).Assembly;

builder.Services.AddCarterWithAssemblies(dormitoryAssembly);

builder.Services.AddMediatRWithAssemblies(dormitoryAssembly);


builder.Services
    .AddDormitoryModule(configuration)
    .AddReservationModule(configuration)
    .AddUserModule(configuration);

var app = builder.Build();
// Configure HTTP request pipeline

app.MapCarter();

app.UseDormitoryModule()
    .UseReservationModule()
    .UseUserModule();

app.Run();