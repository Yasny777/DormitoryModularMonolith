// INITIALIZE BUILDER
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// CONFIGURATION
var configuration = builder.Configuration;
// HOST
builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.ReadFrom.Configuration(ctx.Configuration);
});

// Configure services
var dormitoryAssembly = typeof(DormitoryModule).Assembly;
var identityAssembly = typeof(IdentityModule).Assembly;
var reservationAssembly = typeof(ReservationModule).Assembly;

// Common services
builder.Services.AddHttpContextAccessor();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

builder.Services.AddMappings();

builder.Services.AddCarterWithAssemblies(dormitoryAssembly, identityAssembly, reservationAssembly);

builder.Services.AddMediatRWithAssemblies(dormitoryAssembly, identityAssembly, reservationAssembly);


// CORS
var frontend = builder.Configuration["Frontend:Url"];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(frontend!) // Adres frontendu
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
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
app.MapCarter();
app.UseDormitoryModule()
    .UseReservationModule();
app.UseIdentityModule();
app.Run();