using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Mediation.Commands;
using Shared.Infrastructure.Mediation.Queries;

namespace Shared.Infrastructure.Mediation
{
    public static class Extensions
    {
        public static IServiceCollection AddMediation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommands();
            services.AddQueries();

            return services;
        }
    }
}
