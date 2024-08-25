using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Application;
using Modules.Orders.Infrastructure;

namespace Modules.Orders.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddOrdersModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOrdersInfrastructure(configuration);
            services.AddOrdersApplication();

            return services;
        }
    }
}
