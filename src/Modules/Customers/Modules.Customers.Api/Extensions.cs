using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Application;
using Modules.Customers.Infrastructure;

namespace Modules.Customers.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomersModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomersInfrastructure(configuration);
            services.AddCustomersApplication(configuration);

            return services;
        }
    }
}
