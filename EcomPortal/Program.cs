using EcomPortal.Controllers;
using EcomPortal.Exceptions;
using EcomPortal.Repositories;
using EcomPortal.Services;
using Microsoft.EntityFrameworkCore;
using EcomPortal.Models.Entities;
using EcomPortal.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
var connectionString = builder.Configuration.GetConnectionString("DockerConnection")
                       ?? throw new InvalidOperationException("Connection string not found");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Register services and repositories
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
builder.Services.AddScoped<ICrudRepository<User>, UserRepository>();
builder.Services.AddScoped<ICrudRepository<Order>, OrderRepository>();
builder.Services.AddScoped<ICrudRepository<Product>, ProductRepository>();
builder.Services.AddScoped<ICrudRepository<OrderProduct>, CrudRepository<OrderProduct>>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

try
{
    Log.Information("Starting the application");

    app.UseExceptionHandler();
    
    // Configure middleware and pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.MapOrderEndpoints();
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application terminated unexpectedly");
}
finally
{
    Log.Information("Shutting down the application");
    Log.CloseAndFlush();
}
