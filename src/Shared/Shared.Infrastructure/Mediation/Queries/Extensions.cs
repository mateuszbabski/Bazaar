using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Abstractions.Mediation.Queries;

namespace Shared.Infrastructure.Mediation.Queries
{
    public static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.TryAddSingleton<IQueryDispatcher, QueryDispatcher>();

            services.Scan(selector =>
            {
                selector.FromCallingAssembly()
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IQueryHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime();
            });

            return services;
        }
    }
}
