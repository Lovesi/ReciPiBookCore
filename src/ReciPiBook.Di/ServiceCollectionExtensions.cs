using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ReciPiBook.Entities;
using ReciPiBook.Repository;
using ReciPiBook.Services;

namespace ReciPiBook.Di
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, string recipesDbConn)
        {
            services.AddEntityFramework()
                .AddDbContext<RecipesDb>(options => options.UseSqlServer(recipesDbConn));
            
            services.AddTransient<IRepository<UnitOfMeasure>, Repository<UnitOfMeasure>>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ITestService, TestService>();
            return services;
        }
    }
}
