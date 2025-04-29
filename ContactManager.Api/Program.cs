using ContactManager.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Access DB Context
builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseSqlServer ( builder.Configuration.GetConnectionString("DefaultConnection") ) );

// Interacting with Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Create Swagger to interact with the program using the API instead of the Frontend
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Contact Manager API",
        Description = "An ASP.NET Core Web API for managing contacts.",
        TermsOfService = new Uri("https://example.com/terms"), // Placeholder for now
        Contact = new OpenApiContact
        {
            Name = "Andreas Bahle",
            Email = "Bahle.Andreas.Thomas@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "Placeholder license",
            Url = new Uri("https://example.com/license") // Placeholder for now
        }
    });
});

var app = builder.Build();

// Database Migrations
using (var scope = app.Services.CreateScope () )
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ContactsDbContext> ();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
