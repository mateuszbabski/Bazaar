using Microsoft.Extensions.DependencyInjection;

namespace Modules.Baskets.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddBasketsApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
