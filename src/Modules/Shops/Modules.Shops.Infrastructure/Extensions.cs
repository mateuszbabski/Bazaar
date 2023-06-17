using Microsoft.Extensions.DependencyInjection;

namespace Modules.Shops.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddShopsInfrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
