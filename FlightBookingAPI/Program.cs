using Microsoft.EntityFrameworkCore;
using FlightBookingAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:3000") // Allow CORS for the React frontend running on localhost
                          .AllowAnyHeader() // Allow any headers from the frontend
                          .AllowAnyMethod()); // Allow all HTTP methods (GET, POST, PUT, DELETE)
});

// Register services for the FlightContext and the SQLite database for flights
builder.Services.AddControllers(); // Enable the use of controllers

// FlightContext registration (Flight management context)
builder.Services.AddDbContext<FlightContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FlightDatabase"))); // Configure SQLite database connection

var app = builder.Build();

// Apply migrations to ensure the database is up to date
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var flightContext = services.GetRequiredService<FlightContext>();

    // Apply migrations automatically at startup to sync the database with models
    flightContext.Database.Migrate();
}

// Enable Swagger only for development or testing environments
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger middleware to generate API documentation
    app.UseSwaggerUI(); // Provide a UI to interact with the API
}

// Use CORS policy to allow communication between frontend and backend
app.UseCors("AllowFrontend");

// Authorization middleware (no authentication configured yet)
app.UseAuthorization();

// Map controllers for API endpoints
app.MapControllers();

// Run the application
app.Run();
