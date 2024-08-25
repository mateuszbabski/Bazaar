using Microsoft.Extensions.DependencyInjection;

namespace Modules.Orders.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddOrdersApplication(this IServiceCollection services)
        {           

            return services;
        }
    }
}
