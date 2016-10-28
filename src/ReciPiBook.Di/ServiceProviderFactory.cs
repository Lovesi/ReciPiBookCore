using System;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReciPiBook.Di
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services, IConfiguration configuration)
        {
            var container = new ServiceContainer();
            services.AddMvc();
            services.RegisterDbContexts(configuration.GetConnectionString("recipibookdb"));
            return container.RegisterConfiguration(configuration)
                            .RegisterRepositories()
                            .RegisterServices()
                            .CreateServiceProvider(services);
        }
    }
}
