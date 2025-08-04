using SGR.Web.Services;
using SGR.Web.Services.Interfaces;

namespace SGR.Web.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddHttpClient<IRestaurantService, RestaurantService>();
            services.AddHttpClient<IMenuCategoryService, MenuCategoryService>();

            return services;
        }
    }
}
