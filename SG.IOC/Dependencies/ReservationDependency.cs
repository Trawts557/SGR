using Microsoft.Extensions.DependencyInjection;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Application.Contracts.Repository.Reservations_and_Orders;
using SGR.Application.Services;
using SGR.Persistence.Repositories;

public static class ReservationDependency
{
    public static void AddReservationDependency(this IServiceCollection service)
    {
        service.AddScoped<IReservationRepository, ReservationRepository>();
        service.AddScoped<IReservationService, ReservationService>();

    }
}
