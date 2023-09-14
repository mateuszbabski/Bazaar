using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Discounts.Infrastructure;
using Modules.Discounts.Application;

namespace Modules.Discounts.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddDiscountsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDiscountsInfrastructure(configuration);
            services.AddDiscountsApplication();

            return services;
        }
    }
}
