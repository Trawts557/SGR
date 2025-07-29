using Microsoft.EntityFrameworkCore;
using SG.IOC.Dependencies;
using SGR.Application.Contracts.Repository;
using SGR.Application.Contracts.Repository.Reservations_and_Orders;
using SGR.Application.Interfaces.Repository;
using SGR.Persistence.IContext;
using SGR.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//Controlador Restaurant 
builder.Services.AddRestaurantDependency();


//Controlador Reservation
builder.Services.AddReservationDependency();


//Controlador MenuCategory
builder.Services.AddMenuCategoryDependency();


//Controlador User
builder.Services.AddScoped<IUserRepository, UserRepository>();


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
