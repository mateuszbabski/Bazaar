using Microsoft.Extensions.DependencyInjection;

namespace Modules.Products.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
