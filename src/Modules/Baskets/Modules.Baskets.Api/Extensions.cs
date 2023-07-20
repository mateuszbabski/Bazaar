using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Baskets.Infrastructure;
using Modules.Baskets.Application;

namespace Modules.Baskets.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddBasketsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBasketsInfrastructure(configuration);
            services.AddBasketsApplication();

            return services;
        }
    }
}
