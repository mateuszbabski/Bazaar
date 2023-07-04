using Microsoft.Extensions.DependencyInjection;

namespace Modules.Products.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddProductsModule(this IServiceCollection services) 
        {
            return services;
        }
    }
}
