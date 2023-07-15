using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Baskets.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddBasketsModule(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddBasketsInfrastructure(configuration);
            //services.AddBasketsApplication();

            return services;
        }
    }
}
