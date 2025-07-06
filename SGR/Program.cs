using Microsoft.EntityFrameworkCore;
using SGR.Application.Contracts.Repository;
using SGR.Application.Contracts.Repository.Reservations_and_Orders;
using SGR.Application.Interfaces.Repository;
using SGR.Persistence.IContext;
using SGR.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Controlador Restaurant 
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();

//Controlador Reservation
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();

//Controlador MenuCategory
builder.Services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
builder.Services.AddTransient<IMenuCategoryRepository, MenuCategoryRepository>();

//Controlador User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddControllers();
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
