var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Configure services

builder.Services
    .AddDormitoryModule(configuration)
    .AddReservationModule(configuration)
    .AddUserModule(configuration);

var app = builder.Build();
// Configure HTTP request pipeline

app.UseDormitoryModule()
    .UseReservationModule()
    .UseUserModule();

app.Run();