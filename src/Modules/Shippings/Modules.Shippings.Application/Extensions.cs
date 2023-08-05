using Microsoft.Extensions.DependencyInjection;
using Modules.Shippings.Application.Contracts;

namespace Modules.Shippings.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddShippingsApplication(this IServiceCollection services)
        {

            return services;
        }
    }
}
