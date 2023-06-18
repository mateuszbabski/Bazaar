using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Shops.Application;
using Modules.Shops.Infrastructure;

namespace Modules.Shops.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddShopsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddShopsInfrastructure(configuration);
            services.AddShopsApplication();

            return services;
        }
    }
}
