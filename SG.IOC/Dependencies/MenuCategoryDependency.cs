using Microsoft.Extensions.DependencyInjection;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Application.Contracts.Repository;
using SGR.Application.Services;
using SGR.Persistence.Repositories;

public static class MenuCategoryDependency
{
    public static void AddMenuCategoryDependency(this IServiceCollection service)
    {
        service.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
        service.AddScoped<IMenuCategoryService, MenuCategoryService>();
    }
}
