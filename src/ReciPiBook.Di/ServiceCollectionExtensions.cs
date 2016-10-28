using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ReciPiBook.Entities;

namespace ReciPiBook.Di
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbContexts(this IServiceCollection services, string recipesDbConn)
        {
            services.AddEntityFramework()
                .AddDbContext<RecipesDb>(options => options.UseSqlServer(recipesDbConn));

            return services;
        }
    }
}
