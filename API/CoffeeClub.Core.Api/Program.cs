using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CoffeeClubContext>(
    options => options.UseSqlServer("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICoffeeBeanRepository, CoffeeBeanRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
