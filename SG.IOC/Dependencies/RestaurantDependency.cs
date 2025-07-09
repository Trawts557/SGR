using Microsoft.Extensions.DependencyInjection;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Application.Interfaces.Repository;
using SGR.Application.Services;
using SGR.Persistence.Repositories;


namespace SG.IOC.Dependencies
{
    public static class RestaurantDependency
    {
        public static void AddRestaurantDependency (this IServiceCollection service)
        {
            service.AddScoped<IRestaurantRepository, RestaurantRepository>();
            service.AddScoped<IRestaurantService, RestaurantService>();

        }
    }
}
