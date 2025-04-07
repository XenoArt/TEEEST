using Microsoft.EntityFrameworkCore;
using TEEEST.Data;
using System;
using TEEEST.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure CORS to allow requests from ANY origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Allow requests from any domain
                  .AllowAnyMethod() // Allow GET, POST, PUT, DELETE, etc.
                  .AllowAnyHeader(); // Allow all headers
        });
});

// Register DbContext with enhanced connection resilience
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("mimdinare"),
        sqlServerOptions =>
        {
            sqlServerOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            sqlServerOptions.CommandTimeout(120); // Increased timeout for potentially slow connections
        }
    ));

// Register services
builder.Services.AddScoped<ICashregService, CashregService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEndOrEditService, EndOrEditService>();
builder.Services.AddScoped<IActiveService, ActiveService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS globally
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Initialize database with better error handling
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Test connection before attempting migration
        if (await db.Database.CanConnectAsync())
        {
            await db.Database.MigrateAsync(); // This will apply any pending migrations
            Console.WriteLine("Database connection successful and migrations applied.");
        }
        else
        {
            Console.WriteLine("Cannot connect to the database. Please check connection string and server availability.");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while connecting to the database: {ex.Message}");
    // Log the full exception details in a production environment
}

await app.RunAsync();
