using System;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ReciPiBook.Entities;
using Microsoft.AspNetCore.Builder;

namespace ReciPiBook.Di
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services, IConfiguration configuration)
        {
            var container = new ServiceContainer();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<RecipesDb>()
                    .AddDefaultTokenProviders();
                    
            services.AddOpenIddict()
                .AddEntityFrameworkCoreStores<RecipesDb>()
                .AddMvcBinders()
                .EnableTokenEndpoint("/api/token")
                .AllowPasswordFlow()
                .DisableHttpsRequirement()
                .AddEphemeralSigningKey();

            services.AddMvc();

            services.RegisterDbContexts(configuration.GetConnectionString("recipibookdb"));

            return container.RegisterConfiguration(configuration)
                            .RegisterRepositories()
                            .RegisterServices()
                            .CreateServiceProvider(services);
        }
    }
}
