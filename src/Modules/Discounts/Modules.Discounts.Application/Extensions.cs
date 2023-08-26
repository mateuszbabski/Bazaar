using Microsoft.Extensions.DependencyInjection;
using Modules.Discounts.Application.Contracts;

namespace Modules.Discounts.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddDiscountsApplication(this IServiceCollection services)
        {

            return services;
        }
    }
}
