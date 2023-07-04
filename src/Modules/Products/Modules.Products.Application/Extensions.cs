using Microsoft.Extensions.DependencyInjection;

namespace Modules.Products.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddProductsApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
