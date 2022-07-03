using Microsoft.EntityFrameworkCore;
using BookManagerApi.Models;
using BookManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBookManagementService, BookManagementService>();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("BookManagerApi");

if (builder.Environment.EnvironmentName == "Testing")
{
    builder.Services.AddDbContext<BookContext>(option =>
        option.UseInMemoryDatabase("BookDb"));
}
else
{
    builder.Services.AddDbContext<BookContext>(option =>
        option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

// Configure Swagger/OpenAPI Documentation
// You can learn more on this link: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() ||
    app.Environment.EnvironmentName == "Testing")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

