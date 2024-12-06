using EcomPortal.Controllers;
using EcomPortal.Data;
using EcomPortal.Middleware;
using EcomPortal.Repositories;
using EcomPortal.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DotNetEnv;
using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.User;
using EcomPortal.Models.Dtos.Order;
using EcomPortal.Models.Dtos.Product;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var envFileName = $".env.{environment.ToLower()}";
DotNetEnv.Env.Load(envFileName);
DotNetEnv.Env.Load();

// Configure logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        .Build())
    .CreateLogger();

Log.Information("Starting up the application...");
var builder = WebApplication.CreateBuilder(args);

Log.CloseAndFlush();

// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IGenericService<User, AddUserDto, UpdateUserDto>, UserService>();
builder.Services.AddScoped<IGenericService<Product, AddProductDto, UpdateProductDto>,ProductService>();
builder.Services.AddScoped<IGenericService<Order, AddOrderDto, UpdateOrderDto>, OrderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapOrderEndpoints();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
