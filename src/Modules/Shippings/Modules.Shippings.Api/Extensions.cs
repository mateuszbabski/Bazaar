using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Shippings.Application;
using Modules.Shippings.Infrastructure;

namespace Modules.Shippings.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddShippingsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddShippingsInfrastructure(configuration);
            services.AddShippingsApplication();

            return services;
        }
    }
}
