using P10_WebApi.Interfaces;
using P10_WebApi.Models;
using P10_WebApi.Middlewares;
using P10_WebApi.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// CORS setup for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// JWT Secret
var SecretKey = builder.Configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("Secret Key not set!");

builder.Services.AddTransient<ITokenService>(_ => new TokenService(SecretKey));
builder.Services.AddScoped<MongoDbService>();

var app = builder.Build();

// Middleware
app.UseMiddleware<ErrorHandler>();

// Enable CORS
app.UseCors("AllowFrontend");

// Uncomment if you want HTTPS redirection
app.UseHttpsRedirection();

// If you have auth middleware
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Test endpoint
app.MapGet("/", () => "API is running...");

app.Run();
